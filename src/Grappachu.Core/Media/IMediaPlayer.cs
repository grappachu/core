namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Extends the <see cref="IPlayable" /> interface with functions for handling different media contents
    /// </summary>
    public interface IMediaPlayer<T> : IPlayable where T : IMediaSource
    {
        /// <summary>
        ///     Gets or sets the media content for the <see cref="IMediaPlayer{T}" />
        /// </summary>
        T Source { get; }

        /// <summary>
        ///     Loads a media item
        /// </summary>
        void Open(T content);

        /// <summary>
        ///     Uunloads the current media item releasing resources
        /// </summary>
        void Close();
    }
}