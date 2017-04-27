namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    /// Rappresenta lo stato di un file 
    /// </summary>
    public enum FileStatus
    {
        /// <summary>
        /// Sconosciuto. Non è possibile verificare il file. Può essere dovuto a problemi di accesso alla risorsa.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Il file non è presente nel percorso specificato
        /// </summary>
        Missing = 1,

        /// <summary>
        /// Il file è presente ma i dati non sono conformi all'hash specificato. Può essere causato da un download interrotto o da un errore di ricezione.
        /// </summary>
        Corrupted = 2,

        /// <summary>
        /// Il file è presente e il contenuto è coerente con l'hash passato.
        /// </summary>
        Present = 3
    }
}