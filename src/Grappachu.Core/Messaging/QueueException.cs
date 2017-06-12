using System;

namespace Grappachu.Core.Messaging
{
    /// <summary>
    ///     Defines a basic exception for messaging errors
    /// </summary>
    public class QueueException : Exception
    {
        /// <summary>
        ///     Creates a new instance of <see cref="QueueException" />
        /// </summary>
        public QueueException()
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="QueueException" />
        /// </summary>
        public QueueException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="QueueException" />
        /// </summary>
        public QueueException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}