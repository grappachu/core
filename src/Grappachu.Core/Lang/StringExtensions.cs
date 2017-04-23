using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Grappachu.Core.Lang
{
    /// <summary>
    /// Estensioni di funzionalità per le stringhe
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert string value to decimal ignore the culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Decimal value.</returns>
        public static decimal ToDecimal(this string value)
        {
            var tempValue = value;

            var punctuation = value.Where(char.IsPunctuation).Distinct().ToArray();
            var count = punctuation.Count();

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

            decimal number = decimal.Parse(tempValue, format);
            return number;
        }
      
        /// <summary>
        /// Swaps the decimal separator chars.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        private static string SwapChar(this string value, char from, char to)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var builder = new StringBuilder();

            foreach (var item in value)
            {
                var c = item;
                if (c == from)
                {c = to;}
                else if (c == to)
                { c = from;}

                builder.Append(c);
            }
            return builder.ToString();
        }

    }
}
