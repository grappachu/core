using System.Globalization;

namespace Grappachu.Core.Globalization
{
    /// <summary>
    /// Componente per la gestione di stringhe localizzate
    /// </summary>
    public class StringHelper
    {
        private readonly CultureInfo _culture;

        private const long KILOBYTE = 1024;
        private const long MEGABYTE = 1024 * KILOBYTE;
        private const long GIGABYTE = 1024 * MEGABYTE;
        private const long TERABYTE = 1024 * GIGABYTE;



        /// <summary>
        /// Crea un componente dipendente dalla cultura per la gestione di stringhe localizzate
        /// </summary>
        /// <param name="culture"></param> 
        public StringHelper(CultureInfo culture)
        {
            _culture = culture;
        }

        /// <summary>
        /// Crea un componente per la gestione di stringhe localizzate basato su InvariantCulture
        /// </summary>
        public StringHelper()
            : this(CultureInfo.InvariantCulture)
        {

        }

        /// <summary>
        /// Ottiene la cultura predefita di questo oggetto
        /// </summary>
        public CultureInfo Culture { get { return _culture; } }

        /// <summary>
        /// Formatta una quantità di byte in una stringa proporzionata all'unità di misura
        /// </summary>
        /// <param name="byteSize"></param>
        /// <returns></returns>
        public string FormatFileSize(long byteSize)
        {
            if (byteSize > TERABYTE) return ((float)byteSize / TERABYTE).ToString("0.00 Tb", _culture);
            if (byteSize > GIGABYTE) return ((float)byteSize / GIGABYTE).ToString("0.00 Gb", _culture);
            if (byteSize > MEGABYTE) return ((float)byteSize / MEGABYTE).ToString("0.00 Mb", _culture);
            if (byteSize > KILOBYTE) return ((float)byteSize / KILOBYTE).ToString("0.00 Kb", _culture);
            return byteSize + " Byte";
        }
    }
}