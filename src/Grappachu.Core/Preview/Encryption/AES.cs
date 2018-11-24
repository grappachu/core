using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Grappachu.Core.Lang.Extensions;
using Grappachu.Core.Lang.Text;

namespace Grappachu.Core.Preview.Encryption
{
    /// <summary>
    ///     Rappresenta un componente per la crittografia di stringhe e file
    /// </summary>
    public class AES : IEncryptionProvider
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int KEYSIZE = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DERIVATION_ITERATIONS = 1000;

        /// <summary>
        ///     Inizializza una nuova istanza di <see cref="AES" />
        /// </summary>
        public AES()
        {
            Algorythm = EncryptionAlgorythm.AES;
        }

        /// <summary>
        ///     Specifica il formato dell'output
        /// </summary>
        public BinaryRepresentation OutputFormat { get; set; }

        /// <summary>
        ///     Ottiene il tipo di algoritmo utilizzato da questo provider
        /// </summary>
        public EncryptionAlgorythm Algorythm { get; }

        /// <summary>
        ///     Codifica un file utilizzando la chiave specificata
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="encryptionKey"></param>
        public void EncryptFile(FileInfo source, FileInfo target, string encryptionKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Decodifica file utilizzando la chiave specificata
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="encryptionKey"></param>
        public void DecryptFile(FileInfo source, FileInfo target, string encryptionKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Codifica una stringa con la chiave specificata
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string encryptionKey)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (var password = new Rfc2898DeriveBytes(encryptionKey, saltStringBytes, DERIVATION_ITERATIONS))
            {
                var keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        var memoryStream = new MemoryStream();
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                            var cipherTextBytes = saltStringBytes;
                            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

                            return cipherTextBytes.ToString(OutputFormat);
                        }

                    }
                }
            }
        }

        /// <summary>
        ///     Decodifica una stringa con la chiave specificata
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedText, string encryptionKey)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = encryptedText.ToByteArray(OutputFormat);

            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KEYSIZE / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KEYSIZE / 8).Take(KEYSIZE / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes =
                cipherTextBytesWithSaltAndIv.Skip(KEYSIZE / 8 * 2)
                    .Take(cipherTextBytesWithSaltAndIv.Length - KEYSIZE / 8 * 2)
                    .ToArray();

            using (var password = new Rfc2898DeriveBytes(encryptionKey, saltStringBytes, DERIVATION_ITERATIONS))
            {
                var keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var cryptoStream = new CryptoStream(new MemoryStream(cipherTextBytes), decryptor, CryptoStreamMode.Read))
                        {
                            var plainTextBytes = new byte[cipherTextBytes.Length];
                            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                        }
                    }
                }
            }
        }


        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
            }

            return randomBytes;
        }
    }
}