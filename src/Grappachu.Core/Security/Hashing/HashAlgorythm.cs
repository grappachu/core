namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    ///     Represents an hashing algorythm
    /// </summary>
    public enum HashAlgorythm
    {
        /// <summary>
        ///     Used for definint custom Hashing algorythms
        /// </summary>
        Custom = 0,

        /// <summary>
        ///     MD5 encoding created by Ronald Rivest in 1991 and defined in RFC 1321.
        /// </summary>
        MD5,

        /// <summary>
        ///     SHA-512 (512 bit) is part of SHA-2 set of cryptographic hash functions, designed by the U.S. National Security
        ///     Agency (NSA) and published in 2001
        /// </summary>
        SHA512
    }
}