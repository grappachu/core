namespace Grappachu.Core.Media.Common
{
    /// <summary>
    ///     Defines the playing state for a <see cref="IPlayable"/> object
    /// </summary>
    public enum MediaState
    {
        /// <summary>
        ///     No media content is available for playing
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