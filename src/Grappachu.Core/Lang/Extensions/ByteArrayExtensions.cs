using System;
using System.Text;
using Grappachu.Core.Lang.Text;

namespace Grappachu.Core.Lang.Extensions
{
    /// <summary>
    ///     Defines a set of extension methods for array of bytes
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        ///     Converts a byte array to a string according to the specified representation
        /// </summary>
        /// <param name="data"></param>
        /// <param name="representation">Specifies the number of bytes used to represent a single char</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">
        ///     This method currently supports only <see cref="BinaryRepresentation.Base64" />
        ///     and <see cref="BinaryRepresentation.Hex" /> encodings
        /// </exception>
        public static string ToString(this byte[] data, BinaryRepresentation representation)
        {
            switch (representation)
            {
                case BinaryRepresentation.Base64:
                    return Convert.ToBase64String(data);
                case BinaryRepresentation.Hex:
                    var sBuilder = new StringBuilder();
                    foreach (var b in data)
                        sBuilder.Append(b.ToString("x2"));
                    return sBuilder.ToString();
            }
            throw new NotSupportedException("Unsupported " + nameof(BinaryRepresentation));
        }


        /// <summary>
        ///     Converts a string into a byte array using the specified representation to perform conversion
        /// </summary>
        /// <param name="text">text to convert</param>
        /// <param name="representation">Specifies the binary representation of the input string</param>
        /// <exception cref="NotSupportedException">
        ///     This method currently supports only <see cref="BinaryRepresentation.Base64" />
        ///     and <see cref="BinaryRepresentation.Hex" /> encodings
        /// </exception>
        /// <returns></returns>
        public static byte[] ToByteArray(this string text, BinaryRepresentation representation)
        {
            byte[] bytes;
            switch (representation)
            {
                case BinaryRepresentation.Base64:
                    bytes = Convert.FromBase64String(text);
                    break;
                case BinaryRepresentation.Hex:
                    bytes = FromHexString(text);
                    break;
                default:
                    throw new NotSupportedException("Unsupported " + nameof(BinaryRepresentation));
            }
            return bytes;
        }


        private static byte[] FromHexString(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}