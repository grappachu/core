namespace Grappachu.Core.Security.Hashing
{
    /// <summary>
    ///    Represents an hashing algorythm
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
        MD5
    }
}