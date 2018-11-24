using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Grappachu.Core.Drawing.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="Image" />
    /// </summary>
    public static class ImageExtension
    {
        private static readonly ImageConverter ImageConverter = new ImageConverter();

        #region Resize 

        /// <summary>
        ///     Resizes an image to fit into the desired Size without preserving aspect ratio
        /// </summary>
        /// <param name="image"></param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <param name="outputSize"></param>
        public static Image Resize(this Image image, Size outputSize,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            var result = new Bitmap(outputSize.Width, outputSize.Height, format);

            //set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = GetGraphics(result, quality))
            {
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            return result;
        }

        #endregion

        ///// <summary>
        /////     Converts an <see cref="Image" /> into <see cref="Bitmap" />.
        ///// If original image cannot be directy casted than it will be converted into a new format
        ///// </summary>
        ///// <param name="img"></param>
        ///// <param name="quality"></param>
        ///// <param name="format"></param>
        ///// <returns></returns>
        //public static Bitmap ToBitmap(this Image img, GraphicsQuality quality = GraphicsQuality.Default, PixelFormat format = PixelFormat.Format32bppArgb)
        //{
        //    // If Can cast then cast
        //    var bmp = img as Bitmap;
        //    if (bmp != null)
        //    {
        //        return bmp;
        //    }

        //    // else create a new bitmap obhect
        //    var result = new Bitmap(img.Width, img.Height, format);
        //    result.SetResolution(img.HorizontalResolution, img.VerticalResolution);
        //    using (var graphics = GetGraphics(result, quality))
        //    {
        //        graphics.DrawImage(img, 0, 0, result.Width, result.Height);
        //    }
        //    return result;
        //}


        /// <summary>
        ///     Creates a graphics object from image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static Graphics GetGraphics(this Image img, GraphicsQuality quality)
        {
            var g = Graphics.FromImage(img);
            switch (quality)
            {
                case GraphicsQuality.Highest:
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    break;
                case GraphicsQuality.Fastest:
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.InterpolationMode = InterpolationMode.Low;
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    break;
                default:
                    g.CompositingQuality = CompositingQuality.Default;
                    g.InterpolationMode = InterpolationMode.Default;
                    g.SmoothingMode = SmoothingMode.Default;
                    break;
            }

            return g;
        }


        /// <summary>
        ///     Converts an image into as specific <see cref="ImageFormat" /> an returns it as byte array.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <seealso cref="GetBytes(Image)" />
        public static byte[] GetBytes(this Image img, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                var imgFormat = format ?? img.RawFormat;
                img.Save(ms, imgFormat);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Gets an image as a Byte array.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <remarks>
        ///     This method is about 20% fastest than <see cref="GetBytes(Image, ImageFormat)" /> on a medium Windows10 based
        ///     pc
        /// </remarks>
        public static byte[] GetBytes(this Image img)
        {
            var xByte = (byte[]) ImageConverter.ConvertTo(img, typeof(byte[]));
            return xByte;
        }

        #region Scale To Fit

        /// <summary>
        ///     Resizes an image proportionally to fit into the desired Size
        /// </summary>
        /// <param name="img"></param>
        /// <param name="format"></param>
        /// <param name="maxSize"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static Image ScaleToFit(this Image img, Size maxSize,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            return ScaleToFit(img, maxSize.Width, maxSize.Height, quality, format);
        }

        /// <summary>
        ///     Resizes an image proportionally to fit into the desired Size
        /// </summary>
        /// <param name="img">Image to resize</param>
        /// <param name="maxWidth">Output maximum width</param>
        /// <param name="maxHeight">Output maximum height</param>
        /// <param name="format"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static Image ScaleToFit(this Image img, int maxWidth, int maxHeight,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            var ratioX = (double) maxWidth / img.Width;
            var ratioY = (double) maxHeight / img.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (img.Width * ratio);
            var newHeight = (int) (img.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight, format);
            using (var g = GetGraphics(newImage, quality))
            {
                g.DrawImage(img, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        #endregion

        #region Scale To Size

        /// <summary>
        ///     Resizes an image proportionally adding borders to fit into the desired Size
        /// </summary>
        /// <param name="img">Image to resize</param>
        /// <param name="outputSize">Output size</param>
        /// <param name="backColor">Background color for the output image</param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image ScaleToSize(this Image img, Size outputSize, Color backColor,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            return ScaleToSize(img, outputSize.Width, outputSize.Height, backColor, quality, format);
        }

        /// <summary>
        ///     Resizes an image proportionally adding borders to fit into the desired Size
        /// </summary>
        /// <param name="img">Image to resize</param>
        /// <param name="width">Output width</param>
        /// <param name="height">Output height</param>
        /// <param name="backColor">Background color for the output image</param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image ScaleToSize(this Image img, int width, int height, Color backColor,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            var sourceWidth = img.Width;
            var sourceHeight = img.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            var destX = 0;
            var destY = 0;

            float nPercent;
            var nPercentW = (float) width / sourceWidth;
            var nPercentH = (float) height / sourceHeight;
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((width - sourceWidth * nPercent) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((height - sourceHeight * nPercent) / 2);
            }

            var destWidth = (int) Math.Min(Math.Ceiling(sourceWidth * nPercent), width);
            var destHeight = (int) Math.Min(Math.Ceiling(sourceHeight * nPercent), height);

            var bmPhoto = new Bitmap(width, height, format);
            bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (var grPhoto = GetGraphics(bmPhoto, quality))
            {
                grPhoto.Clear(backColor);
                grPhoto.DrawImage(img,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }

            return bmPhoto;
        }

        #endregion

        #region Scale To Fill

        /// <summary>
        ///     Resizes proportionally and crops an image to fill the desired Size
        /// </summary>
        /// <param name="imgPhoto"></param>
        /// <param name="outputSize"></param>
        /// <param name="alignment"></param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image ScaleToFill(this Image imgPhoto, Size outputSize,
            ContentAlignment alignment = ContentAlignment.MiddleCenter,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            return ScaleToFill(imgPhoto, outputSize.Width, outputSize.Height, alignment, quality, format);
        }

        /// <summary>
        ///     Resizes proportionally and crops an image to fill the desired Size
        /// </summary>
        /// <param name="imgPhoto"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="alignment"></param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image ScaleToFill(this Image imgPhoto, int width, int height,
            ContentAlignment alignment = ContentAlignment.MiddleCenter,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            var sourceWidth = imgPhoto.Width;
            var sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            var destX = 0;
            var destY = 0;

            float nPercent;

            var nPercentW = (float) width / sourceWidth;
            var nPercentH = (float) height / sourceHeight;

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (alignment)
                {
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        destY = 0;
                        break;
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        destY = (int)
                            (height - sourceHeight * nPercent);
                        break;
                    default:
                        destY = (int)
                            ((height - sourceHeight * nPercent) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (alignment)
                {
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.TopLeft:
                        destX = 0;
                        break;
                    case ContentAlignment.BottomRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.TopRight:
                        destX = (int)
                            (width - sourceWidth * nPercent);
                        break;
                    default:
                        destX = (int)
                            ((width - sourceWidth * nPercent) / 2);
                        break;
                }
            }

            var destWidth = (int) (sourceWidth * nPercent);
            var destHeight = (int) (sourceHeight * nPercent);

            var bmPhoto = new Bitmap(width, height, format);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            using (var grPhoto = GetGraphics(bmPhoto, quality))
            {
                grPhoto.DrawImage(imgPhoto,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }

            return bmPhoto;
        }

        #endregion

        #region EnlargeCanvas

        /// <summary>
        ///     Enlarge the image background using the specified color
        /// </summary>
        /// <param name="img"></param>
        /// <param name="size"></param>
        /// <param name="backColor"></param>
        /// <param name="imgAlignment"></param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image EnlargeCanvas(this Image img, Size size, Color backColor,
            ContentAlignment imgAlignment = ContentAlignment.MiddleCenter,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            return EnlargeCanvas(img, size.Width, size.Height, backColor, imgAlignment, quality, format);
        }

        /// <summary>
        ///     Enlarge the image background using the specified color
        /// </summary>
        /// <param name="img"></param>
        /// <param name="backColor"></param>
        /// <param name="imgAlignment"></param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image EnlargeCanvas(this Image img, int width, int height, Color backColor,
            ContentAlignment imgAlignment = ContentAlignment.MiddleCenter,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            if (img.Width > width || img.Height > height)
            {
                throw new InvalidOperationException("Image size is bigger than canvas target size");
            }

            var bmPhoto = new Bitmap(width, height, format);
            bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (var grPhoto = GetGraphics(bmPhoto, quality))
            {
                grPhoto.Clear(backColor);
                int destX, destY;
                switch (imgAlignment)
                {
                    case ContentAlignment.BottomCenter:
                        destX = (width - img.Width) / 2;
                        destY = height - img.Height;
                        break;
                    case ContentAlignment.BottomLeft:
                        destX = 0;
                        destY = height - img.Height;
                        break;
                    case ContentAlignment.BottomRight:
                        destX = width - img.Width;
                        destY = height - img.Height;
                        break;
                    case ContentAlignment.MiddleCenter:
                        destX = (width - img.Width) / 2;
                        destY = (height - img.Height) / 2;
                        break;
                    case ContentAlignment.MiddleLeft:
                        destX = 0;
                        destY = (height - img.Height) / 2;
                        break;
                    case ContentAlignment.MiddleRight:
                        destX = width - img.Width;
                        destY = (height - img.Height) / 2;
                        break;
                    case ContentAlignment.TopCenter:
                        destX = (width - img.Width) / 2;
                        destY = 0;
                        break;
                    case ContentAlignment.TopLeft:
                        destX = 0;
                        destY = 0;
                        break;
                    case ContentAlignment.TopRight:
                        destX = width - img.Width;
                        destY = 0;
                        break;
                    default:
                        destX = 0;
                        destY = 0;
                        break;
                }

                grPhoto.DrawImage(img,
                    new Rectangle(destX, destY, img.Width, img.Height),
                    new Rectangle(0, 0, img.Width, img.Height),
                    GraphicsUnit.Pixel);
            }

            return bmPhoto;
        }

        /// <summary>
        ///     Adds a Border to the image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="borderThickness">Width of the border (px)</param>
        /// <param name="borderColor">Color of the border</param>
        /// <param name="quality"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Image EnlargeCanvas(this Image img, int borderThickness, Color borderColor,
            GraphicsQuality quality = GraphicsQuality.Default,
            PixelFormat format = PixelFormat.Format32bppArgb)
        {
            var width = img.Width + 2 * borderThickness;
            var height = img.Height + 2 * borderThickness;
            return img.EnlargeCanvas(width, height, borderColor,
                ContentAlignment.MiddleCenter, quality, format);
        }

        #endregion
    }
}


//    /// <summary>
//    ///     A quick lookup for getting image encoders
//    /// </summary>
//    private static Dictionary<string, ImageCodecInfo> _encoders;

//        /// <summary>
//        ///     A quick lookup for getting image encoders
//        /// </summary>
//        private static Dictionary<string, ImageCodecInfo> Encoders
//        {
//            //get accessor that creates the dictionary on demand
//            get
//            {
//                //if the quick lookup isn't initialised, initialise it
//                if (_encoders == null)
//                {
//                    _encoders = new Dictionary<string, ImageCodecInfo>();
//                }

//                //if there are no codecs, try loading them
//                if (_encoders.Count == 0)
//                {
//                    //get all the codecs
//                    foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
//                    {
//                        //add each codec to the quick lookup
//                        _encoders.Add(codec.MimeType.ToLower(), codec);
//                    }
//                }

//                //return the lookup
//                return _encoders;
//            }
//        }


//        /// <summary>
//        ///     Saves an image as a jpeg image, with the given quality
//        /// </summary>
//        /// <param name="path">Path to which the image would be saved.</param>
//        /// <param name="image"></param>
//        /// <param name="quality">
//        ///     An integer from 0 to 100, with 100 being the
//        ///     highest quality
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        ///     An invalid value was entered for image quality.
//        /// </exception>
//        public static void SaveJpeg(this Image image, string path, int quality)
//        {
//            //ensure the quality is within the correct range
//            if ((quality < 0) || (quality > 100))
//            {
//                //create the error message
//                string error = string.Format("Jpeg image quality must be between 0 and 100, " +
//                                             "with 100 being the highest quality.  " +
//                                             "A value of {0} was specified.", quality);
//                //throw a helpful exception
//                throw new ArgumentOutOfRangeException(error);
//            }

//            //create an encoder parameter for the image quality
//            using (var qualityParam = new EncoderParameter(Encoder.Quality, quality))
//            {
//                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

//                //create a collection of all parameters that we will pass to the encoder
//                using (var encoderParams = new EncoderParameters(1))
//                {
//                    encoderParams.Param[0] = qualityParam;

//                    //save the image using the codec and the parameters
//                    image.Save(path, jpegCodec, encoderParams);
//                }
//            }
//        }

//        /// <summary>
//        ///     Returns the image codec with the given mime type
//        /// </summary>
//        private static ImageCodecInfo GetEncoderInfo(string mimeType)
//        {
//            //do a case insensitive search for the mime type
//            string lookupKey = mimeType.ToLower();

//            //the codec to return, default to null
//            ImageCodecInfo foundCodec = null;

//            //if we have the encoder, get it to return
//            if (Encoders.ContainsKey(lookupKey))
//            {
//                //pull the codec from the lookup
//                foundCodec = Encoders[lookupKey];
//            }

//            return foundCodec;
//        }
//    }
//}