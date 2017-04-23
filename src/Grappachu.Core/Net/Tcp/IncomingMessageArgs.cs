using System.Net;
using Grappachu.Core.Domain;

namespace Grappachu.Core.Net.Tcp
{
  
    /// <summary>
    /// Rappresenta i dati di un messaggio ricevuto
    /// </summary>
    public class IncomingMessageArgs : MessageArgs
    {
        /// <summary>
        /// Inzializza una nuova istanza di IncomingMessageArgs
        /// </summary> 
        public IncomingMessageArgs(string message, IPAddress senderAddress)
            : base(message)
        {
            SenderAddress = senderAddress;
        }

        /// <summary>
        /// Ottiene l'indirizzo Ip del client remoto
        /// </summary>
        public IPAddress SenderAddress { get; }
    }
}