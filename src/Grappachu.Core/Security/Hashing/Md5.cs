using System.IO;
using System.Text;

namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    ///     Defines a component for computing hashes based on MD5 Algorythm
    /// </summary>
    public class MD5 : IHashProvider
    {
        #region Implementation of IHashProvider

        /// <summary>
        ///     Gests the hashing algorythm used by this provider to compute hashes
        /// </summary>
        public HashAlgorythm Algorythm => HashAlgorythm.MD5;


        /// <summary>
        ///     Computes the hash for a file using the MD5 Algorythm
        /// </summary>
        /// <param name="file">File to be hashed</param>
        /// <returns></returns>
        public byte[] Hash(FileInfo file)
        {
            return MD5File(file);
        }


        /// <summary>
        ///     Computes the hash for a string using the MD5 Algorythm
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] Hash(string text)
        {
            return MD5Bytes(Encoding.Default.GetBytes(text));
        }


        /// <summary>
        ///     Computes the hash for a byte array using the MD5 Algorythm
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Hash(byte[] bytes)
        {
            return MD5Bytes(bytes);
        }

        #endregion

        #region Private Methods

        private static byte[] MD5Bytes(byte[] bytes)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            byte[] data;
            using (var md5Hasher = System.Security.Cryptography.MD5.Create())
            {
                data = md5Hasher.ComputeHash(bytes);
            }

            return data;
        }

        private static byte[] MD5File(FileInfo file)
        {
            byte[] mHash;
            using (var fs = file.OpenRead())
            {
                // definizione del nostro tipo
                using (var sscMd5 = System.Security.Cryptography.MD5.Create())
                {
                    mHash = sscMd5.ComputeHash(fs);
                }
            }
            return mHash;
        }

        #endregion
    }
}