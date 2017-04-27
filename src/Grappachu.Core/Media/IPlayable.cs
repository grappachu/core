using System;
using System.ComponentModel;
using Grappachu.Core.Media.Common;

namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Defines a component that implements basic playback functions (Play/Stop/Pause)
    /// </summary>
    public interface IPlayable : INotifyPropertyChanged
    {
        /// <summary>
        ///     Gets the state of this playable object
        /// </summary>
        MediaState Status { get; }

        /// <summary>
        ///     Invoked at end of media 
        /// </summary>
        event EventHandler MediaEnded;

        /// <summary>
        ///     Invoked whend the <see cref="IPlayable"/> object changes its state
        /// </summary>
        event EventHandler<MediaStateEventArgs> MediaStateChanged;

        /// <summary>
        ///     Starts the playback
        /// </summary>
        void Play();

        /// <summary>
        ///     Stops the playback
        /// </summary>
        void Stop();

        /// <summary>
        ///     Puts the playback in a paused state
        /// </summary>
        void Pause();
    }
}