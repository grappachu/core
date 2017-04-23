using System;
using System.IO;
using System.Text;

namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    /// Componente per l'hashing con algoritmo Md5
    /// </summary>
    public class MD5 : IHashProvider
    {

        #region Implementation of IHashProvider

        /// <summary>
        /// Specifica il formato dell'output
        /// </summary>
        public OutputEncoding OutputFormat { get; set; }

        /// <summary>
        /// Ottiene il tipo di algoritmo utilizzato da questo provider
        /// </summary>
        public HashAlgorythm Algorythm { get { return HashAlgorythm.MD5; } }

        /// <summary>
        /// Calcola l'hash di un file utilizzando l'algoritmo MD5
        /// </summary>
        /// <param name="filename">File di cui calcorale l'hash</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string HashFile(string filename)
        {
            return MD5File(filename);
        }

        /// <summary>
        /// Calcola l'hash di una stringa utilizzando l'algoritmo MD5
        /// </summary>
        public string HashString(string value)
        {
            return MD5Bytes(Encoding.Default.GetBytes(value));
        }

        /// <summary>
        /// Calcola l'hash di un array di byte utilizzando l'algoritmo MD5
        /// </summary> 
        public string HashBytes(byte[] bytes)
        {
            return MD5Bytes(bytes);
        }

        #endregion

        #region Private Methods

        private string Print(byte[] hashbytes)
        {
            switch (OutputFormat)
            {
                case OutputEncoding.Base64:
                    return Convert.ToBase64String(hashbytes);
                case OutputEncoding.Hex:
                    var sBuilder = new StringBuilder();
                    foreach (byte b in hashbytes)
                    {
                        sBuilder.Append(b.ToString("x2"));
                    }
                    return sBuilder.ToString();
            }
            throw new NotSupportedException("Unknown encoding");
        }

        private string MD5Bytes(byte[] bytes)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            byte[] data;
            using (var md5Hasher = System.Security.Cryptography.MD5.Create())
            {
                data = md5Hasher.ComputeHash(bytes);
            }

            return Print(data);
        }

        private string MD5File(string filename)
        {
            string retValue;
            using (var fs = File.OpenRead(filename))
            {
                // definizione del nostro tipo
                byte[] mHash;
                using (var sscMd5 = System.Security.Cryptography.MD5.Create())
                {
                    mHash = sscMd5.ComputeHash(fs);
                }

                retValue = Print(mHash);
            }
            return retValue;
        }

        #endregion


    }
}
