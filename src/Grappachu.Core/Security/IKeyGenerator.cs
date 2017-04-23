namespace Grappachu.Core.Security
{
    /// <summary>
    /// Rappresenta un generico generatore di chiavi per un oggetto
    /// </summary>
    public interface IKeyGenerator<in T, out TK>
    {
        /// <summary>
        /// Genera una chiave unica per l'oggetto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TK GenerateKey(T obj);
    }
}