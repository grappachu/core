using System;

namespace Grappachu.Core.Domain
{
    /// <summary>
    /// Rappresenta una classe generica da utilizzare per gli eventi di errore
    /// </summary>
    public class ErrorArgs : EventArgs
    {
        /// <summary>
        /// Ottiene o imposta il messaggio di errore
        /// </summary>
        public string Messsage { get; set; }

        /// <summary>
        /// Ottiene o imposta una descrizione dettagliata dell'errore
        /// </summary>
        public string Detail { get; set; }


        /// <summary>
        /// Inizializza una nuova istanza della classe ErrorArgs
        /// </summary>
        public ErrorArgs()
        {
        }

        /// <summary>
        /// Inizializza una nuova istanza della classe ErrorArgs
        /// </summary>
        /// <param name="ex"></param>
        public ErrorArgs(Exception ex)
        {
            Messsage = ex.Message;
            Detail = ex.ToString();
        }
    }
}
