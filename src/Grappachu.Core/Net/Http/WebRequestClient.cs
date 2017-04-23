using System;
using System.IO;
using System.Net;
using System.Text;

namespace Grappachu.Core.Net.Http
{

    /// <summary>
    /// Rappresenta un client per richieste basate su protocollo Http
    /// </summary>
    public class WebRequestClient : IWebRequestClient
    {
        /// <summary>
        /// Ottiene o imposta un timeout predefinito per tutte le richieste.  
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        /// Inizializza una nuova istanza della classe WebRequestClient
        /// </summary>
        public WebRequestClient()
        {
            DefaultTimeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Esegue una WebRequest in POST e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string Post(System.Security.Policy.Url url, string postData)
        {
            var wr = WebRequest.Create(url.Value);
            wr.Method = "POST";
            var byteArray = Encoding.UTF8.GetBytes(postData ?? string.Empty);
            wr.ContentType = "application/x-www-form-urlencoded";
            wr.ContentLength = byteArray.Length;
            wr.Timeout = (int)DefaultTimeout.TotalMilliseconds;
            using (var requestStream = wr.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
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

      
        /// <summary>
        /// Esegue una WebRequest in GET e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <returns></returns>
        public string Get(System.Security.Policy.Url url)
        {
            var wr = WebRequest.Create(url.Value);
            wr.Method = "GET";
            wr.Timeout = (int)DefaultTimeout.TotalMilliseconds;

            // Get the response.
            using (var response = wr.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        return responseFromServer;
                    }
                }
            }
            return null;
        }
    }
}