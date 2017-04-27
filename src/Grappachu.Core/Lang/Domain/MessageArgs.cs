using System;

namespace Grappachu.Core.Lang.Domain
{
    /// <summary>
    ///     Represents a basic class that contain a simple text event data.
    /// </summary>
    public class MessageArgs : EventArgs
    {
        /// <summary>
        ///     Creates a new instance of <see cref="MessageArgs" />
        /// </summary>
        /// <param name="message"></param>
        public MessageArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Gets or sets the message content
        /// </summary>
        public string Message { get; set; }
    }
}