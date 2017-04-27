﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace Grappachu.Core.Preview.Text
{
    /// <summary>
    ///     Adds extension methods for manipulating strings
    /// </summary>
    public static class StringExtension
    {
        private static readonly char[] BreackableChars = {' ', '-', '|'};
        private static readonly char[] NewlineChars = {'\n', '\r'};

        /// <summary>
        ///     Wraps a text over multiple lines by inserting newline char when required.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLineLength">max length of lines in chars</param>
        public static string Wrap(this string text, int maxLineLength = 80)
        {
            var lineLength = 0;
            var lastBreakableIndex = -1;

            var sb = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                sb.Append(text[i]);
                lineLength++;

                if (NewlineChars.Contains(text[i]))
                    lineLength = 0;
                if (BreackableChars.Contains(text[i]))
                    lastBreakableIndex = i;

                if (lineLength > maxLineLength)
                {
                    // Should Break 
                    var charsBack = i - lastBreakableIndex;
                    if (charsBack < lineLength)
                    {
                        sb.Insert(sb.Length - charsBack, System.Environment.NewLine);
                        lineLength = 0;
                    }
                    else
                    {
                        sb.Append(System.Environment.NewLine);
                        lineLength = 0;
                    }
                }
            }
            return sb.ToString();
        }

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