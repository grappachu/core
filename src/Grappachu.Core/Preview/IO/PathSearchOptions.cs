using System;
using System.IO;

namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    /// Rappresenta un insieme di opzioni per l'enumerazione dei file all'interno di un percorso
    /// </summary>
    [Serializable]
    public class PathSearchOptions
    {
        /// <summary>
        /// Ottiene o imposta un filtro per i file da elencare.
        /// Default: *.*
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Specifica la profondità della ricerca
        /// </summary>
        public SearchOption SearchOption { get; set; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="PathSearchOptions"/>
        /// </summary>
        internal PathSearchOptions()
            : this(@"*.*", SearchOption.TopDirectoryOnly)
        {
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="PathSearchOptions"/>
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="searchOption"></param>
        internal PathSearchOptions(string pattern, SearchOption searchOption)
        {
            Filter = pattern;
            SearchOption = searchOption;
        }
    }
}