using System.IO;

namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    ///     Defines a component for compute hash of data
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        ///     Gests the hashing algorythm used by this provider to compute hashes
        /// </summary>
        HashAlgorythm Algorythm { get; }

        /// <summary>
        ///     Computes the hash for a file
        /// </summary>
        /// <param name="file">File to be hashed</param>
        /// <returns></returns>
        byte[] Hash(FileInfo file);

        /// <summary>
        ///     Computes the hash for a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        byte[] Hash(string text);

        /// <summary>
        ///     Computes the hash for a byte array
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        byte[] Hash(byte[] bytes);
    }
}