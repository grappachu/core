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
        private const string DEFAULT_FORMAT = "g";

        private const long KILOBYTE = 1024;
        private const long MEGABYTE = 1024 * KILOBYTE;
        private const long GIGABYTE = 1024 * MEGABYTE;
        private const long TERABYTE = 1024 * GIGABYTE;
        private const long PETABYTE = 1024 * TERABYTE;
        private const decimal EXABYTE = 1024 * PETABYTE;
        private const decimal ETTABYTE = 1024 * EXABYTE;
        private const decimal YOTTABYTE = 1024 * ETTABYTE;


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
                format = DEFAULT_FORMAT;

            if (format == DEFAULT_FORMAT)
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
            if (byteSize > YOTTABYTE) return ((float) byteSize / TERABYTE).ToString("0.00 Yb", Culture);
            if (byteSize > ETTABYTE) return ((float) byteSize / TERABYTE).ToString("0.00 Zb", Culture);
            if (byteSize > EXABYTE) return ((float) byteSize / TERABYTE).ToString("0.00 Eb", Culture);
            if (byteSize > PETABYTE) return ((float) byteSize / TERABYTE).ToString("0.00 Pb", Culture);
            if (byteSize > TERABYTE) return ((float) byteSize / TERABYTE).ToString("0.00 Tb", Culture);
            if (byteSize > GIGABYTE) return ((float) byteSize / GIGABYTE).ToString("0.00 Gb", Culture);
            if (byteSize > MEGABYTE) return ((float) byteSize / MEGABYTE).ToString("0.00 Mb", Culture);
            if (byteSize > KILOBYTE) return ((float) byteSize / KILOBYTE).ToString("0.00 Kb", Culture);
            return byteSize + " Byte";
        }
    }
}