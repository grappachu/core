namespace Grappachu.Core.Media.Common
{
    /// <summary>
    ///     Defines a play order for media contents
    /// </summary>
    public enum PlayOrder
    {
        /// <summary>
        ///     The contents are played from the first to the last
        /// </summary>
        Forward = 0,

        /// <summary>
        ///     Contents are played in reverse order from last to first
        /// </summary>
        Reverse = 1,

        /// <summary>
        ///     The contents are played in random order
        /// </summary>
        Random = 2
    }
}