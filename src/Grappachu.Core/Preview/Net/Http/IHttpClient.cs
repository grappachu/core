using System;
using System.Collections.Generic;
using System.Net;

namespace Grappachu.Core.Preview.Net.Http
{
    /// <summary>
    ///     Rappresenta un client per richieste basate su protocollo Http
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        ///     Esegue una WebRequest in POST e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="credentials">Imposta delle credenziali di rete per l'autenticazione</param>
        /// <param name="timeoutMills">Imposta un timeout per la richiesta.</param>
        /// <returns></returns>
        string Post(Uri url, HttpPostMessage postData, IDictionary<string, string> headers = null,
            NetworkCredential credentials = null, int timeoutMills = 0);

        /// <summary>
        ///     Esegue una WebRequest in GET e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <param name="headers"></param>
        /// <param name="credentials">Imposta delle credenziali di rete per l'autenticazione</param>
        /// <param name="timeoutMills">Imposta un timeout per la richiesta.</param>
        /// <returns></returns>
        string Get(Uri url, IDictionary<string, string> headers = null,
            NetworkCredential credentials = null, int timeoutMills = 0);
    }
}