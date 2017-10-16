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
        /// <returns></returns>
        public static Image GetImage(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
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