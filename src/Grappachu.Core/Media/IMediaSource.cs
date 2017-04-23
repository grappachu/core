namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Rappresenta un contenuto multimediale
    /// </summary>
    public interface IMediaSource
    {
        /// <summary>
        /// Ottiene un identificativo del contenuto multimediale
        /// </summary>
        string Path { get; }
    }
}