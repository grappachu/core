namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Rappresenta uno stato di riproduzione di un contenuto multimediale
    /// </summary>
    public enum MediaState
    {
        /// <summary>
        ///     Nessun contenuto multimediale è caricato
        /// </summary>
        Waiting,

        /// <summary>
        ///     The current media item is getting additional data before start playing
        /// </summary>
        Loading,

        /// <summary>
        ///     Ready to begin playing.
        /// </summary>
        Ready,

        /// <summary>
        ///     The current media item is playing.
        /// </summary>
        Playing,

        /// <summary>
        ///     Playback of the current media item is paused. When a media item is paused, resuming playback begins from the same
        ///     location.
        /// </summary>
        Paused,

        /// <summary>
        ///     Playback of the current media item is stopped
        /// </summary>
        Stopped
    }
}