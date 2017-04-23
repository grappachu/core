using System;
using System.IO;

namespace Grappachu.Core.IO
{
    /// <summary>
    /// Rappresenta un percorso di ricerca per l'enumerazione di file.
    /// </summary>
    [Serializable]
    public class PathSearchInfo
    {
        /// <summary>
        /// Ottiene o imposta la radice del percorso da cui enumerare i file
        /// </summary>
        public DirectoryInfo Path { get; private set; }

        /// <summary>
        /// Ottiene le impostazioni per l'enumerazione dei file
        /// </summary>
        public PathSearchOptions Options { get; private set; }

        #region Constructors

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="PathSearchInfo"/> 
        /// </summary>
        /// <param name="rootPath">Percorso radice della ricerca</param>
        public PathSearchInfo(string rootPath)
        {
            Path = new DirectoryInfo(rootPath);
            Options = new PathSearchOptions();
        }
        
        /// <summary>
        /// Inizializza una nuova istanza di <see cref="PathSearchInfo"/> 
        /// </summary>
        /// <param name="rootPath">Percorso radice della ricerca</param>
        /// <param name="searchOption"></param> 
        public PathSearchInfo(string rootPath, SearchOption searchOption)
            : this(rootPath)
        {
            Options.SearchOption = searchOption;
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="PathSearchInfo"/> 
        /// </summary>
        /// <param name="rootPath">Percorso radice della ricerca</param>
        /// <param name="filter"></param>
        /// <param name="searchOption"></param>
        public PathSearchInfo(string rootPath, string filter, SearchOption searchOption)
            : this(rootPath)
        {
            Options.Filter = filter;
            Options.SearchOption = searchOption;
        }

        #endregion
    }
}