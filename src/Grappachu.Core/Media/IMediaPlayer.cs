namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Rappresenta un componente per la riproduzione di elementi multimediali
    /// </summary>
    public interface IMediaPlayer : ISeekable
    {
        /// <summary>
        ///     Verifica se un contenutò è supportato e può essere caricato
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        bool CanOpen(IMediaSource content);

        /// <summary>
        ///     Carica un contenuto
        /// </summary>
        /// <param name="content">Contenuto da caricare</param>
        void Open(IMediaSource content);

        /// <summary>
        ///     Scarica il contenuto e rilascia le risorse associate a questo oggetto
        /// </summary>
        void Close();
    }
}