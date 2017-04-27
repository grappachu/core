namespace Grappachu.Core.Preview.Net.Http
{
    /// <summary>
    ///     Rappresenta un messagio in post
    /// </summary>
    public class HttpPostMessage
    {
        /// <summary>
        ///     Crea una nuova istanza di HttpPostMessage
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="messageData"></param>
        public HttpPostMessage(HttpPostMethod contentType, string messageData)
        {
            ContentType = contentType;
            MessageData = messageData;
        }

        /// <summary>
        ///     Ottiene il tipo di messaggio
        /// </summary>
        public HttpPostMethod ContentType { get; private set; }

        /// <summary>
        ///     Ottiene i dati del messaggio
        /// </summary>
        public string MessageData { get; private set; }
    }
}