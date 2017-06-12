namespace Grappachu.Core.Messaging
{
    /// <summary>
    ///     Defines a basic publisher for messages
    /// </summary>
    /// <typeparam name="T">Type of message to be published</typeparam>
    public interface IPublisher<in T>
    {
        /// <summary>
        ///     Publishes a message to a queue
        /// </summary>
        /// <param name="message"></param>
        void Publish(T message);
    }
}