
using System;

namespace Grappachu.Core.Media
{
    /// <summary>
    /// Rappresenta il cambio di stato di un player multimediale
    /// </summary>
    public class MediaStateEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public MediaStateEventArgs(MediaState state)
        {
            State = state;
        }

        /// <summary>
        /// Nuovo stato multimediale
        /// </summary>
        public MediaState State { get; }
    }
}