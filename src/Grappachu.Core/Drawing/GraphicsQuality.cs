namespace Grappachu.Core.Drawing
{
    /// <summary>
    ///     Defines a group of settings for image quality operations
    /// </summary>
    public enum GraphicsQuality
    {
        /// <summary>
        ///     Default system settings
        /// </summary>
        Default = 0,

        /// <summary>
        ///     All settings must maximize the quality
        /// </summary>
        Highest,

        /// <summary>
        ///     All settings must maximize the performance
        /// </summary>
        Fastest,

        /// <summary>
        ///     User will provide custom settings
        /// </summary>
        Custom
    }
}