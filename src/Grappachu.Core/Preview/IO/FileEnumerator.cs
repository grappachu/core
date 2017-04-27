using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    ///     Enumeratore di file da percorsi multipli
    /// </summary>
    public class FileEnumerator : IFileEnumerator
    {
     //   private readonly ILog _log = LogManager.GetLogger(typeof (FileEnumerator));

        /// <summary>
        ///     Ottiene l'elenco dei percorsi da esplorare
        /// </summary>
        public ICollection<PathSearchInfo> Paths { get; }

        #region Constructors

        /// <summary>
        ///     Crea una nuova istanza di FileEnumerator
        /// </summary>
        public FileEnumerator()
        {
            Paths = new List<PathSearchInfo>();
        }

        /// <summary>
        ///     Crea una nuova istanza di FileEnumerator
        /// </summary>
        /// <param name="paths"></param>
        public FileEnumerator(ICollection<PathSearchInfo> paths)
        {
            Paths = paths;
        }

        #endregion

        /// <summary>
        ///     Restituisce un enumeratore che consente di scorrere l'insieme.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FileInfo> GetEnumerator()
        {
            return OnEnumerateItems().GetEnumerator();
        }

        /// <summary>
        ///     Restituisce l'enumeratore che scorre l'insieme.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<FileInfo> OnEnumerateItems()
        {
            //_log.Debug("Enumerating files");
            var files = new Dictionary<string, FileInfo>();
            foreach (PathSearchInfo path in Paths)
            {
                EnumeratePath(path, ref files);
            }
        //    _log.Debug(string.Format("Enumeration completed ({0} items found).", files.Count));

            return files.Values;
        }


        private void EnumeratePath(PathSearchInfo root, ref Dictionary<string, FileInfo> files)
        {
            //_log.DebugFormat("Exploring {0} [Filter:{1}|Recursive:{2}]", root.Path, root.Options.Filter,
             //   root.Options.SearchOption);

            DirectoryInfo dir = root.Path;
            PathSearchOptions options = root.Options;

            // This method assumes that the application has discovery permissions 
            // for all folders under the specified path.
            IEnumerable<FileInfo> fileList = dir.GetFiles(options.Filter, options.SearchOption);
            foreach (FileInfo info in fileList)
            {
                if (!files.ContainsKey(info.FullName))
                {
                    files.Add(info.FullName, info);
                }
            }
        }
    }
}