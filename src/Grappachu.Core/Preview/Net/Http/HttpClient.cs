using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Grappachu.Core.Lang.Extensions;

namespace Grappachu.Core.Preview.Net.Http
{
    /// <summary>
    ///     Rappresenta un client per richieste basate su protocollo Http
    /// </summary>
    public class HttpClient : IHttpClient
    {
        /// <summary>
        ///     Inizializza una nuova istanza della classe WebRequestClient
        /// </summary>
        public HttpClient()
        {
            DefaultTimeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        ///     Ottiene o imposta un timeout predefinito per tutte le richieste.
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        ///     Esegue una WebRequest in POST e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="credentials">Imposta delle credenziali di rete per l'autenticazione</param>
        /// <param name="timeoutMills">
        ///     Imposta un timeout per la richiesta. Se non viene specificato (=0) sarà applicato il
        ///     <see cref="DefaultTimeout" />
        /// </param>
        /// <returns></returns>
        public string Post(Uri url, HttpPostMessage postData, IDictionary<string, string> headers = null,
            NetworkCredential credentials = null, int timeoutMills = 0)
        {
            var wr = WebRequest.Create(url);
            wr.Method = WebRequestMethods.Http.Post;
            var bytes = Encoding.UTF8.GetBytes(postData.MessageData ?? string.Empty);
            wr.ContentType = postData.ContentType.GetDescription();
            wr.ContentLength = bytes.Length;
            wr.Timeout = timeoutMills > 0 ? timeoutMills : (int) DefaultTimeout.TotalMilliseconds;

            return OnDoRequest(wr, bytes, headers, credentials);
        }


        /// <summary>
        ///     Esegue una WebRequest in GET e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <param name="headers"></param>
        /// <param name="credentials">Imposta delle credenziali di rete per l'autenticazione</param>
        /// <param name="timeoutMills">
        ///     Imposta un timeout per la richiesta. Se non viene specificato (=0) sarà applicato il
        ///     <see cref="DefaultTimeout" />
        /// </param>
        /// <returns></returns>
        public string Get(Uri url, IDictionary<string, string> headers = null,
            NetworkCredential credentials = null, int timeoutMills = 0)
        {
            var wr = WebRequest.Create(url);
            wr.Method = WebRequestMethods.Http.Get;
            wr.Timeout = timeoutMills > 0 ? timeoutMills : (int) DefaultTimeout.TotalMilliseconds;

            return OnDoRequest(wr, null, headers, credentials);
        }


        private static string OnDoRequest(WebRequest wr, byte[] dataBytes,
            IDictionary<string, string> headers,
            NetworkCredential credentials)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    wr.Headers.Add(header.Key, header.Value);
                }
            }

            if (credentials != null)
            {
                wr.Credentials = credentials;
            }

            if (dataBytes != null)
            {
                using (var requestStream = wr.GetRequestStream())
                {
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
                }
            }

            // Get the response.
            using (var response = wr.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var responseFromServer = reader.ReadToEnd();
                        return responseFromServer;
                    }
                }
            }
            return null;
        }
    }
}