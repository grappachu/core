namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    /// Rappresenta un componente per l'hashing di stringhe e file
    /// </summary>
    public interface IHashProvider
    {

        /// <summary>
        /// Specifica il formato dell'output
        /// </summary>
        OutputEncoding OutputFormat { get; set; }

        /// <summary>
        /// Ottiene il tipo di algoritmo utilizzato da questo provider
        /// </summary>
        HashAlgorythm Algorythm { get; }

        /// <summary>
        /// Esegue l'hash di un file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string HashFile(string filename);

        /// <summary>
        /// Esegue l'hash di una stringa
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string HashString(string text);

        /// <summary>
        /// Esegue l'hash di un array di byte
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string HashBytes(byte[] bytes);

    }
}