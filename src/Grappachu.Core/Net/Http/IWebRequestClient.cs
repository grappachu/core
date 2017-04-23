namespace Grappachu.Core.Net.Http
{
    /// <summary>
    /// Rappresenta un client per richieste basate su protocollo Http
    /// </summary>
    public interface IWebRequestClient
    {
        /// <summary>
        /// Esegue una WebRequest in POST e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <param name="postData"></param>
        /// <returns></returns>
        string Post(System.Security.Policy.Url url, string postData);

        /// <summary>
        /// Esegue una WebRequest in GET e restituisce il risultato.
        /// </summary>
        /// <param name="url">Url della richiesta</param>
        /// <returns></returns>
        string Get(System.Security.Policy.Url url);
    }
}
