namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    ///     Defines a generic component for generating hash keys from objects.
    /// </summary>
    public interface IKeyGenerator<in T, out TK>
    {
        /// <summary>
        ///     Genera una chiave unica per l'oggetto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TK GenerateKey(T obj);
    }
}