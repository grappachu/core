using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Grappachu.Core.Lang.Extensions
{
    /// <summary>
    ///     Extension methods for manipulating  <see cref="string" /> objects
    /// </summary>
    public static class StringExtensions
    {
        private static readonly char[] BreackableChars = {' ', '-', '|'};
        private static readonly char[] NewlineChars = {'\n', '\r'};

        /// <summary>
        ///     Convert string value to decimal from any culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Decimal value.</returns>
        public static decimal ToDecimal(this string value)
        {
            var tempValue = value;

            var punctuation = value.Where(char.IsPunctuation).Distinct().ToArray();
            var count = punctuation.Length;

            var format = CultureInfo.InvariantCulture.NumberFormat;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    tempValue = value.Replace(",", ".");
                    break;
                case 2:
                    if (punctuation.ElementAt(0) == '.')
                        tempValue = value.SwapChar('.', ',');
                    break;
                default:
                    throw new InvalidCastException();
            }

            var number = decimal.Parse(tempValue, format);
            return number;
        }


        /// <summary>
        ///     Convert string value to double from any culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Decimal value.</returns>
        public static double ToDouble(this string value)
        {
            var tempValue = value;

            var punctuation = value.Where(char.IsPunctuation).Distinct().ToArray();
            var count = punctuation.Length;

            var format = CultureInfo.InvariantCulture.NumberFormat;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    tempValue = value.Replace(",", ".");
                    break;
                case 2:
                    if (punctuation.ElementAt(0) == '.')
                        tempValue = value.SwapChar('.', ',');
                    break;
                default:
                    throw new InvalidCastException();
            }

            var number = double.Parse(tempValue, format);
            return number;
        }


        /// <summary>
        ///     Swaps the decimal separator chars.
        /// </summary>
        private static string SwapChar(this string value, char from, char to)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var builder = new StringBuilder();
            foreach (var item in value)
            {
                var c = item;
                if (c == from)
                    c = to;
                else if (c == to)
                    c = from;

                builder.Append(c);
            }
            return builder.ToString();
        }

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
                        sb.Insert(sb.Length - charsBack, Environment.NewLine);
                        lineLength = 0;
                    }
                    else
                    {
                        sb.Append(Environment.NewLine);
                        lineLength = 0;
                    }
                }
            }
            return sb.ToString();
        }
    }
}