using System;
using System.Globalization;

namespace Grappachu.Core.Globalization.Providers
{
    /// <summary>
    ///     Defines a component for localized filesize formatting. 
    /// </summary>
    /// <example>
    ///     Example of usage:
    ///     <code>
    ///    long bytes = 123456;
    ///    var formattedSize = String.Format(new FileSizeFormatter(), "{0}", bytes);
    /// </code>
    /// </example>
    public sealed class FileSizeFormatter : IFormatProvider, ICustomFormatter
    {
        private const string DefaultFormat = "g";

        private const long Kilobyte = 1024;
        private const long Megabyte = 1024 * Kilobyte;
        private const long Gigabyte = 1024 * Megabyte;
        private const long Terabyte = 1024 * Gigabyte;
        private const long Petabyte = 1024 * Terabyte;
        private const decimal Exabyte = 1024 * Petabyte;
        private const decimal Ettabyte = 1024 * Exabyte;
        private const decimal Yottabyte = 1024 * Ettabyte;


        /// <summary>
        ///     Defines a component for filesize formatting according to a specified culture
        /// </summary>
        /// <param name="culture"></param>
        public FileSizeFormatter(CultureInfo culture)
        {
            Culture = culture;
        }

        /// <summary>
        ///     Defines a component for filesize formatting according to <see cref="CultureInfo.InvariantCulture" />
        /// </summary>
        public FileSizeFormatter()
            : this(CultureInfo.InvariantCulture)
        {
        }

        /// <summary>
        ///     Gets the culture for this provider
        /// </summary>
        public CultureInfo Culture { get; }


        /// <inheritdoc />
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg));

            if (string.IsNullOrEmpty(format))
                format = DefaultFormat;

            if (format == DefaultFormat)
            {
                long data;
                if (long.TryParse(arg.ToString(), out data))
                    return DoFormat(data);
                throw new FormatException(string.Format("'{0}' cannot be used to format {1}.", format, arg));
            }

            var formattable = arg as IFormattable;
            if (formattable != null)
                return formattable.ToString(format, formatProvider);

            return arg.ToString();
        }


        /// <inheritdoc />
        public object GetFormat(Type formatType)
        {
            return typeof(ICustomFormatter) == formatType ? this : null;
        }

        private string DoFormat(long byteSize)
        {
            if (byteSize > Yottabyte) return ((float) byteSize / Terabyte).ToString("0.00 Yb", Culture);
            if (byteSize > Ettabyte) return ((float) byteSize / Terabyte).ToString("0.00 Zb", Culture);
            if (byteSize > Exabyte) return ((float) byteSize / Terabyte).ToString("0.00 Eb", Culture);
            if (byteSize > Petabyte) return ((float) byteSize / Terabyte).ToString("0.00 Pb", Culture);
            if (byteSize > Terabyte) return ((float) byteSize / Terabyte).ToString("0.00 Tb", Culture);
            if (byteSize > Gigabyte) return ((float) byteSize / Gigabyte).ToString("0.00 Gb", Culture);
            if (byteSize > Megabyte) return ((float) byteSize / Megabyte).ToString("0.00 Mb", Culture);
            if (byteSize > Kilobyte) return ((float) byteSize / Kilobyte).ToString("0.00 Kb", Culture);
            return byteSize + " Byte";
        }
    }
}