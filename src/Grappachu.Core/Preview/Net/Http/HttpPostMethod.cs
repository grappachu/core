using System.ComponentModel;
using Grappachu.Core.Lang.Extensions;

namespace Grappachu.Core.Preview.Net.Http
{
    /// <summary>
    ///     Rappresenta
    /// </summary>
    public enum HttpPostMethod
    {
        /// <summary>
        /// </summary>
        [Description("application/x-www-form-urlencoded")] FormUrlEncoded = 0,

        /// <summary>
        /// </summary>
        [Description("multipart/form-data")] MultipartFormData = 1
    }

    /// <summary>
    ///     Rappresenta un estensione di funzionalità per <see cref="HttpPostMethod" />
    /// </summary>
    public static class HttpPostMethodExtension
    {
        /// <summary>
        ///     Restituisce il valore di HttpContentType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToStringValue(this HttpPostMethod type)
        {
            return type.GetDescription();
        }
    }
}