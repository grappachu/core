using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Grappachu.Core.Preview.Text
{
    /// <summary>
    ///     Adds extension methods for manipulating strings
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        ///     Renders a text to a bitmap
        /// </summary>
        /// <param name="input"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this string input, Font font = null, Brush brush = null)
        {
            var objBmpImage = new Bitmap(1, 1);

            // Create  defaults
            var objFont = font ?? SystemFonts.DefaultFont;
            var objBrush = brush ?? new SolidBrush(Color.Black);

            // Create a graphics object to measure the text's width and height.
            var objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            var stringSize = objGraphics.MeasureString(input, objFont);

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, stringSize.ToSize());

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(input, objFont, objBrush, 0, 0);
            objGraphics.Flush();
            return objBmpImage;
        }


        /// <summary>
        ///     Checks if a string matches a text using a string comparison option
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}