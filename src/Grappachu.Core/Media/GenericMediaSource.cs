namespace Grappachu.Core.Media
{
    /// <summary>
    /// Rappresenta un generico contenuto multimediale identificato 
    /// </summary>
    public class GenericMediaSource : IMediaSource
    {
        /// <summary>
        ///  Inizializza un nuovo contenuto multimediale identificato da un Path
        /// </summary>
        /// <param name="path"></param>
        public GenericMediaSource(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Ottiene un identificativo del contenuto multimediale
        /// </summary>
        public string Path { get; }
    }
}