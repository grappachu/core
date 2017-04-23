using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using Grappachu.Core.Domain;
using Timer = System.Timers.Timer;

namespace Grappachu.Core.Net.Tcp
{
    /// <summary>
    /// Definisce un componente in grado di ricevere messaggi TcpIp su Socket.
    /// </summary>
    public class SocketListener : ISocketListener, IDisposable
    {

        private const double TimerPeriodMills = 100.0; 

        private int _port;
        private IPAddress _address;

        private readonly Timer _timer;
        private TcpListener _listener;

        #region Events

        /// <summary>
        /// Richiamato al ricevimento di un messaggio.
        /// </summary>
        public event EventHandler<IncomingMessageArgs> MessageReceived;

        /// <summary>
        /// Richiamato sul verificarsi di un errore.
        /// </summary>
        public event EventHandler<ErrorArgs> Error;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnError(ErrorArgs e)
        {
            Error?.Invoke(this, e);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMessageReceived(IncomingMessageArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        #endregion


        /// <summary>
        /// Ottiene o imposta una porta per l'ascolto.
        /// </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                if (IsListening)
                {
                    throw new InvalidOperationException("Not allowed when listening");
                }
                _port = value;
            }
        }

        /// <summary>
        /// Ottiene o imposta un indirizzo IP per l'ascolto.
        /// </summary>
        public IPAddress Address
        {
            get { return _address; }
            set
            {
                if (IsListening)
                {
                    throw new InvalidOperationException("Not allowed when listening");
                }
                _address = value;
            }
        }

        /// <summary>
        /// Ottiene un valore che indica se il componente è in ascolto.
        /// </summary>
        public bool IsListening => _timer.Enabled;

        /// <summary>
        /// Inizializza una nuova istanza di SocketListener
        /// </summary>
        /// <param name="address">Indirizzo IP su cui ricevere messaggi</param>
        /// <param name="port">Porta su cui ricevere messaggi</param>
        public SocketListener(IPAddress address, int port)
        {
            _address = address;
            _port = port;

            _timer = new Timer(TimerPeriodMills);
            _timer.Elapsed += OnAcceptClients;
        }

        private void OnAcceptClients(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (_listener.Pending())
                {
                    _timer.Enabled = false;
                    var thread = new Thread(OnConnect);
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                OnError(new ErrorArgs(ex));
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        private void OnConnect()
        {
            try
            {
                using (var client = _listener.AcceptSocket())
                {
                    byte[] buffer = null;
                    try
                    {
                        if (client.Available == 0)
                        {
                            buffer = Encoding.Default.GetBytes(string.Empty);
                        }
                        else
                        {
                            int bi = 0;
                            while (client.Available > 0)
                            {
                                Array.Resize(ref buffer, bi + 1);
                                client.Receive(buffer, bi, 1, SocketFlags.None);
                                bi += 1;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        OnError(new ErrorArgs(e));
                    }

                    if (buffer != null)
                    {
                        var message = Encoding.Default.GetString(buffer);
                        var rep = (IPEndPoint)client.RemoteEndPoint;

                        // Verifico se ci sono messaggi nulli
                        var args = new IncomingMessageArgs(message, rep.Address);
                        OnMessageReceived(args);

                    }
                }
            }
            catch (Exception ex)
            {
                OnError(new ErrorArgs(ex));
            }
        }

        /// <summary>
        /// Avvia l'ascolto per la ricezione di messaggi.
        /// </summary>
        public void Start()
        {
            _listener = new TcpListener(_address, _port);
            _listener.Start();
            _timer.Enabled = true;
        }

        /// <summary>
        /// Termina l'ascolto per la ricezione di messaggi.
        /// </summary>
        public void Stop()
        {
            _timer.Enabled = false;
            _listener.Stop();
        }


        #region implementation of IDisposeable

        /// <summary>
        ///  Chiude il Socket e rilascia tutte le risorse
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Chiude il Socket e rilascia tutte le risorse
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_listener?.Server != null)
                {
                    if (_listener.Server.Connected)
                    {
                        _listener.Server.Disconnect(true);
                    }
                    _listener.Server.Close();
                }
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Dispose();
                }
            }
        }

        #endregion
    }
}
