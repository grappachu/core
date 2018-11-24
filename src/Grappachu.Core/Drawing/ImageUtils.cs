using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Grappachu.Core.Drawing
{
    /// <summary>
    ///     Extension methods for handling images
    /// </summary>
    public static class ImageUtils
    {
        /// <summary>
        ///     Gets an image from a byte array
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="useEmbeddedColorManagement">
        ///     Specifies whether the new Image object applies color correction according to
        ///     color management information that is embedded in the stream. Embedded information can include International Color
        ///     Consortium (ICC) profiles, gamma values, and chromaticity information. TRUE specifies that color correction is
        ///     enabled, and FALSE specifies that color correction is not enabled. The default value is FALSE
        /// </param>
        /// <returns></returns>
        public static Image GetImage(byte[] imageBytes, bool useEmbeddedColorManagement = false)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms, useEmbeddedColorManagement);
            }
        }

        /// <summary>
        ///     Gets an image from a file
        /// </summary>
        /// <param name="imagePath">The full path of the image to load</param>
        /// <param name="useEmbeddedColorManagement">
        ///     Specifies whether the new Image object applies color correction according to
        ///     color management information that is embedded in the stream. Embedded information can include International Color
        ///     Consortium (ICC) profiles, gamma values, and chromaticity information. TRUE specifies that color correction is
        ///     enabled, and FALSE specifies that color correction is not enabled. The default value is FALSE
        /// </param>
        /// <returns></returns>
        public static Image GetImage(string imagePath, bool useEmbeddedColorManagement = false)
        {
            using (var ms = File.OpenRead(imagePath))
            {
                return Image.FromStream(ms, useEmbeddedColorManagement);
            }
        }

        /// <summary>
        ///     Converts an image to a base64 string representation according to image format
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToBase64(this Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                var imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}