using System;

namespace Grappachu.Core.Domain
{
    /// <summary>
    /// Rappresenta una classe generica da utilizzare per gli eventi che
    /// richiedono l'invio di una stringa. 
    /// </summary>
    public class MessageArgs : EventArgs
    {
        /// <summary>
        /// Ottiene o imposta il messaggio di errore
        /// </summary>
        public string Message { get; set; }

          
        /// <summary>
        /// Inizializza una nuova istanza della classe MessageArgs
        /// </summary>
        /// <param name="message"></param>
        public MessageArgs(string message)
        {
            Message = message; 
        }
    }
}