using System.IO;
using System.Text;

namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    ///     Defines a component for computing hashes based on SHA512 Algorythm
    /// </summary>
    public class SHA512 : IHashProvider
    {
        #region Implementation of IHashProvider

        /// <summary>
        ///     Gests the hashing algorythm used by this provider to compute hashes
        /// </summary>
        public HashAlgorythm Algorythm => HashAlgorythm.SHA512;


        /// <summary>
        ///     Computes the hash for a file using the SHA512 Algorythm
        /// </summary>
        /// <param name="file">File to be hashed</param>
        /// <returns></returns>
        public byte[] Hash(FileInfo file)
        {
            return SHA512File(file);
        }


        /// <summary>
        ///     Computes the hash for a string using the SHA512 Algorythm
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] Hash(string text)
        {
            return SHA512Bytes(Encoding.Default.GetBytes(text));
        }


        /// <summary>
        ///     Computes the hash for a byte array using the SHA512 Algorythm
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Hash(byte[] bytes)
        {
            return SHA512Bytes(bytes);
        }

        #endregion

        #region Private Methods

        private static byte[] SHA512Bytes(byte[] bytes)
        {
            // Create a new instance of the SHA512CryptoServiceProvider object.
            byte[] data;
            using (var sha512Hasher = System.Security.Cryptography.SHA512.Create())
            {
                data = sha512Hasher.ComputeHash(bytes);
            }

            return data;
        }

        private static byte[] SHA512File(FileInfo file)
        {
            byte[] mHash;
            using (var fs = file.OpenRead())
            {
                // definizione del nostro tipo
                using (var sscSha512 = System.Security.Cryptography.SHA512.Create())
                {
                    mHash = sscSha512.ComputeHash(fs);
                }
            }
            return mHash;
        }


        private static SHA512 GetInstance()
        {
            return new SHA512();
        }

        #endregion

        /// <summary>
        /// Provides a static interface to hash a string
        /// </summary>
        /// <param name="textToHash">string to hash</param>
        /// <returns></returns>
        public static byte[] HashString(string textToHash)
        {
            return GetInstance().Hash(textToHash);
        }

        /// <summary>
        /// Provides a static interface to hash a string
        /// </summary>
        /// <param name="fileToHash">file to hash</param>
        /// <returns></returns>
        public static byte[] HashFile(FileInfo fileToHash)
        {
            return GetInstance().Hash(fileToHash);
        }

        /// <summary>
        /// Provides a static interface to hash an array of bytes
        /// </summary>
        /// <param name="bytesToHash">data to hash</param>
        /// <returns></returns>
        public static byte[] HashBytes(byte[] bytesToHash)
        {
            return GetInstance().Hash(bytesToHash);
        }

    }
}