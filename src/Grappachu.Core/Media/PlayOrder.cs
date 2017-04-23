namespace Grappachu.Core.Media
{
    /// <summary>
    /// Rappresenta un ordine di riproduzione per i contenuti
    /// </summary>
    public enum PlayOrder
    {
        /// <summary>
        /// I contenuti sono riprodotti dal primo all'ultimo
        /// </summary>
        Forward = 0, 

        /// <summary>
        /// I contenuti sono riprodotti in ordine inverso dall'ultimo al primo
        /// </summary>
        Reverse = 1, 

        /// <summary>
        /// I contenuti sono riprodotti in ordine casuale
        /// </summary>
        Random = 2
    }
}
