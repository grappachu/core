using System;

namespace Grappachu.Core.Media.Common
{
    /// <summary>
    ///     Defines the base class for status change of a media player
    /// </summary>
    public class MediaStateEventArgs : EventArgs
    {
        /// <summary>
        ///     creates a new instance of <see cref="MediaStateEventArgs" />
        /// </summary>
        /// <param name="state"></param>
        public MediaStateEventArgs(MediaState state)
        {
            State = state;
        }

        /// <summary>
        ///     Gets the new media state for the sender
        /// </summary>
        public MediaState State { get; }
    }
}