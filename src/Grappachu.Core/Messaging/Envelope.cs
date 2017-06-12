using System;

namespace Grappachu.Core.Messaging
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Envelope<T> : EventArgs
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Envelope{T}" />
        /// </summary>
        /// <param name="messageId">A unique id for the message</param>
        /// <param name="message">the message</param>
        public Envelope(string messageId, T message)
        {
            MessageId = messageId;
            Message = message;
        }

        /// <summary>
        ///     Indica se per questo messaggio verrà inviato Ack (true) o Nack (false)
        /// </summary>
        public bool? GiveAck { get; set; }

        /// <summary>
        ///     Gets the id for the message in this envelope
        /// </summary>
        public string MessageId { get; }

        /// <summary>
        ///     Gets the message in this envelope
        /// </summary>
        public T Message { get; private set; }
    }
}