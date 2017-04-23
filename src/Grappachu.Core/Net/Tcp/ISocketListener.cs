using System;
using System.Net;

namespace Grappachu.Core.Net.Tcp
{
    /// <summary>
    /// Definisce un componente in grado di ricevere messaggi su un Socket.
    /// </summary>
    public interface ISocketListener
    {
        /// <summary>
        /// Richiamato al ricevimento di un messaggio.
        /// </summary>
        event EventHandler<IncomingMessageArgs> MessageReceived;

        /// <summary>
        /// Ottiene o imposta una porta per l'ascolto.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Ottiene o imposta un indirizzo IP per l'ascolto.
        /// </summary>
        IPAddress Address { get; set; }

        /// <summary>
        /// Avvia l'ascolto per la ricezione di messaggi.
        /// </summary>
        void Start();

        /// <summary>
        /// Termina l'ascolto per la ricezione di messaggi.
        /// </summary>
        void Stop();

        /// <summary>
        /// Ottiene un valore che indica se il componente è in ascolto.
        /// </summary>
        bool IsListening { get; }

    }
}