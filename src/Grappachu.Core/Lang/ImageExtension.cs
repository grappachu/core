using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Grappachu.Core.Lang
{
    /// <summary>
    ///     Estensioni di funzionalità per <see cref="Image" />
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        ///     A quick lookup for getting image encoders
        /// </summary>
        private static Dictionary<string, ImageCodecInfo> _encoders;

        /// <summary>
        ///     A quick lookup for getting image encoders
        /// </summary>
        private static Dictionary<string, ImageCodecInfo> Encoders
        {
            //get accessor that creates the dictionary on demand
            get
            {
                //if the quick lookup isn't initialised, initialise it
                if (_encoders == null)
                {
                    _encoders = new Dictionary<string, ImageCodecInfo>();
                }

                //if there are no codecs, try loading them
                if (_encoders.Count == 0)
                {
                    //get all the codecs
                    foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                    {
                        //add each codec to the quick lookup
                        _encoders.Add(codec.MimeType.ToLower(), codec);
                    }
                }

                //return the lookup
                return _encoders;
            }
        }


        /// <summary>
        ///     Ridimensiona un'immagine alla larghezza e altezza specificate.
        /// </summary>
        /// <param name="image">Immagine da ridimensionare</param>
        /// <param name="width">Larghezza in pixel dell'immagine di output</param>
        /// <param name="height">Altezza in pixel dell'immagine di output</param>
        public static Image Resize(this Image image, int width, int height)
        {
            return Resize(image, width, height, image.PixelFormat);
        }

        /// <summary>
        ///     Ridimensiona un'immagine alla larghezza e altezza specificate.
        /// </summary>
        /// <param name="image">Immagine da ridimensionare</param>
        /// <param name="width">Larghezza in pixel dell'immagine di output</param>
        /// <param name="height">Altezza in pixel dell'immagine di output</param>
        /// <param name="format">Formato dei dati relativi al colore</param>
        public static Image Resize(this Image image, int width, int height, PixelFormat format)
        {
            //a holder for the result
            var result = new Bitmap(width, height, format);
            //set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap
            return result;
        }

        /// <summary>
        ///     Ridimensiona un'immagine in percentuale
        /// </summary>
        /// <param name="image">Immagine da ridimensionare</param>
        /// <param name="percent">Percentuale dell'immagine finale rispetto all'originale</param>
        public static Image Resize(Image image, float percent)
        {
            var width = (int) (image.Width*(percent));
            var height = (int) (image.Height*(percent));
            return Resize(image, width, height);
        }


        /// <summary>
        ///     Scala un'immagine in modo proporzionale fino ad adattarla al riquadro di destinazione
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth">Larghezza massima dell'immagine di output</param>
        /// <param name="maxHeight">Altezza massima dell'immagine di output</param>
        public static Image Scale(this Image image, int maxWidth, int maxHeight)
        {
            return Scale(image, maxWidth, maxHeight, image.PixelFormat);
        }

        /// <summary>
        ///     Scala un'immagine in modo proporzionale fino ad adattarla al riquadro di destinazione
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth">Larghezza massima dell'immagine di output</param>
        /// <param name="maxHeight">Altezza massima dell'immagine di output</param>
        /// <param name="format">Formato dei dati relativi al colore</param>
        public static Image Scale(this Image image, int maxWidth, int maxHeight, PixelFormat format)
        {
            double ratioX = (double) maxWidth/image.Width;
            double ratioY = (double) maxHeight/image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (image.Width*ratio);
            var newHeight = (int) (image.Height*ratio);

            var bmPhoto = new Bitmap(newWidth, newHeight, format);
            bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return bmPhoto;
        }

         
        /// <summary>
        ///     Ridimensiona un'immagine adattandola ad un'area specificata ritagliando i bordi
        /// </summary>
        /// <param name="imgPhoto"></param>
        /// <param name="width">Larghezza dell'immagine di output</param>
        /// <param name="height">Altezza dell'immagine di output</param>
        /// <param name="anchor">Allineamento del ritaglio</param>
        /// <returns></returns>
        /// <remarks>
        ///     L'immagine risultante mantiene la stessa profondità
        ///     di colore dell'immagine originale
        /// </remarks>
        public static Image ScaleWithCrop(this Image imgPhoto, int width, int height, ContentAlignment anchor)
        {
            return ScaleWithCrop(imgPhoto, width, height, anchor, imgPhoto.PixelFormat);
        }


        /// <summary>
        ///     Ridimensiona un'immagine adattandola ad un'area specificata ritagliando i bordi
        /// </summary>
        /// <param name="imgPhoto"></param>
        /// <param name="width">Larghezza dell'immagine di output</param>
        /// <param name="height">Altezza dell'immagine di output</param>
        /// <param name="anchor">Allineamento del ritaglio</param>
        /// <param name="format">Formato dell'immagine</param>
        /// <returns></returns>
        /// <remarks>
        ///     L'immagine risultante mantiene la stessa profondità
        ///     di colore dell'immagine originale
        /// </remarks>
        public static Image ScaleWithCrop(Image imgPhoto, int width, int height, ContentAlignment anchor,
            PixelFormat format)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int destX = 0;
            int destY = 0;

            float nPercent;

            float nPercentW = ((float) width/sourceWidth);
            float nPercentH = ((float) height/sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (anchor)
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
                            (height - (sourceHeight*nPercent));
                        break;
                    default:
                        destY = (int)
                            ((height - (sourceHeight*nPercent))/2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (anchor)
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
                            (width - (sourceWidth*nPercent));
                        break;
                    default:
                        destX = (int)
                            ((width - (sourceWidth*nPercent))/2);
                        break;
                }
            }

            var destWidth = (int) (sourceWidth*nPercent);
            var destHeight = (int) (sourceHeight*nPercent);

            var bmPhoto = new Bitmap(width, height, format);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(0, 0, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }


        /// <summary>
        ///     Aggiunge un contorno all'immagine
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width">Larghezza dell'immagine di output (Deve essere maggiore dell'immagine di input).</param>
        /// <param name="height">Altezza dell'immagine di output (Deve essere maggiore dell'immagine di input).</param>
        /// <param name="canvasColor">Colore del bordo</param>
        /// <param name="alignment">Posizione dell'immagine sorgente</param>
        /// <returns></returns>
        public static Image EnlargeCanvas(this Image image, int width, int height, Color canvasColor,
            ContentAlignment alignment)
        {
            return EnlargeCanvas(image, width, height, canvasColor, alignment, image.PixelFormat);
        }

        /// <summary>
        ///     Aggiunge un contorno all'immagine
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width">Larghezza dell'immagine di output (Deve essere maggiore dell'immagine di input).</param>
        /// <param name="height">Altezza dell'immagine di output (Deve essere maggiore dell'immagine di input).</param>
        /// <param name="canvasColor">Colore del bordo</param>
        /// <param name="alignment">Posizione dell'immagine sorgente</param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Image EnlargeCanvas(this Image image, int width, int height, Color canvasColor,
            ContentAlignment alignment, PixelFormat format)
        {
            if (image.Width > width || image.Height > height)
            {
                throw new InvalidOperationException("Image size is bigger than canvas target size");
            }
            var bmPhoto = new Bitmap(width, height, format);
            bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.Clear(canvasColor);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                int destX, destY;
                switch (alignment)
                {
                    case ContentAlignment.BottomCenter:
                        destX = (width - image.Width)/2;
                        destY = height - image.Height;
                        break;
                    case ContentAlignment.BottomLeft:
                        destX = 0;
                        destY = height - image.Height;
                        break;
                    case ContentAlignment.BottomRight:
                        destX = width - image.Width;
                        destY = height - image.Height;
                        break;
                    case ContentAlignment.MiddleCenter:
                        destX = (width - image.Width)/2;
                        destY = (height - image.Height)/2;
                        break;
                    case ContentAlignment.MiddleLeft:
                        destX = 0;
                        destY = (height - image.Height)/2;
                        break;
                    case ContentAlignment.MiddleRight:
                        destX = width - image.Width;
                        destY = (height - image.Height)/2;
                        break;
                    case ContentAlignment.TopCenter:
                        destX = (width - image.Width)/2;
                        destY = 0;
                        break;
                    case ContentAlignment.TopLeft:
                        destX = 0;
                        destY = 0;
                        break;
                    case ContentAlignment.TopRight:
                        destX = width - image.Width;
                        destY = 0;
                        break;
                    default:
                        destX = 0;
                        destY = 0;
                        break;
                }

                grPhoto.DrawImage(image,
                    new Rectangle(destX, destY, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            return bmPhoto;
        }


        /// <summary>
        ///     Saves an image as a jpeg image, with the given quality
        /// </summary>
        /// <param name="path">Path to which the image would be saved.</param>
        /// <param name="image"></param>
        /// <param name="quality">
        ///     An integer from 0 to 100, with 100 being the
        ///     highest quality
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     An invalid value was entered for image quality.
        /// </exception>
        public static void SaveJpeg(this Image image, string path, int quality)
        {
            //ensure the quality is within the correct range
            if ((quality < 0) || (quality > 100))
            {
                //create the error message
                string error = string.Format("Jpeg image quality must be between 0 and 100, " +
                                             "with 100 being the highest quality.  " +
                                             "A value of {0} was specified.", quality);
                //throw a helpful exception
                throw new ArgumentOutOfRangeException(error);
            }

            //create an encoder parameter for the image quality
            using (var qualityParam = new EncoderParameter(Encoder.Quality, quality))
            {
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

                //create a collection of all parameters that we will pass to the encoder
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = qualityParam;

                    //save the image using the codec and the parameters
                    image.Save(path, jpegCodec, encoderParams);
                }
            }
        }

        /// <summary>
        ///     Returns the image codec with the given mime type
        /// </summary>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            //do a case insensitive search for the mime type
            string lookupKey = mimeType.ToLower();

            //the codec to return, default to null
            ImageCodecInfo foundCodec = null;

            //if we have the encoder, get it to return
            if (Encoders.ContainsKey(lookupKey))
            {
                //pull the codec from the lookup
                foundCodec = Encoders[lookupKey];
            }

            return foundCodec;
        }
    }
}