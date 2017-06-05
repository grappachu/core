using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Grappachu.Core.Lang.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="String" /> objects
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Convert string value to decimal ignore the culture.
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
    }
}