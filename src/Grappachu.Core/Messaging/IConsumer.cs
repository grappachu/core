using System;

namespace Grappachu.Core.Messaging
{
    /// <summary>
    ///     Defines a basic interface for a consumer
    /// </summary>
    /// <typeparam name="T">Type of message to be consumed</typeparam>
    public interface IConsumer<T> : IDisposable
    {
        /// <summary>
        /// Invoked when a message has been received
        /// </summary>
        event EventHandler<Envelope<T>> MessageReceived;

        /// <summary>
        /// Start to consume messages  
        /// </summary>
        void BeginConsume();

        /// <summary>
        /// Prevent this component from consuming messages  
        /// </summary>
        void EndConsume();
    }
}