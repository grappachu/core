using System.IO;

namespace Grappachu.Core.Preview.Encryption
{
    /// <summary>
    ///     Defines a component for data encryption
    /// </summary>
    public interface IEncryptionProvider
    {
        /// <summary>
        ///     Gets the encryption algorythm used by this provider
        /// </summary>
        EncryptionAlgorythm Algorythm { get; }


        /// <summary>
        ///     Encrypts a file using the <paramref name="encryptionKey"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="encryptionKey">A user-defined key used for encrypt data</param>
        void EncryptFile(FileInfo source, FileInfo target, string encryptionKey);

        /// <summary>
        ///     Decrypts a file using the defined key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="encryptionKey"></param>
        void DecryptFile(FileInfo source, FileInfo target, string encryptionKey);


        /// <summary>
        ///     Codifica una stringa con la chiave specificata
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        string Encrypt(string plainText, string encryptionKey);

        /// <summary>
        ///     Decodifica una stringa con la chiave specificata
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        string Decrypt(string encryptedText, string encryptionKey);
    }
}