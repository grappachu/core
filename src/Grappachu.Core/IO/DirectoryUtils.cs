using System.IO;
using System.Linq;

namespace Grappachu.Core.IO
{
    /// <summary>
    ///     Defines a set of utility methods and extensions to work with filesystem folders
    /// </summary>
    public static class DirectoryUtils
    {
        /// <summary>
        ///     Checks if a directory contains any file or folder.
        /// </summary>
        /// <param name="pathToCheck">The full directory path to check</param>
        /// <param name="ignoreEmptyFolders">When true this method will consider empty only directories with no files</param>
        /// <returns></returns>
        public static bool IsEmpty(this DirectoryInfo pathToCheck, bool ignoreEmptyFolders = false)
        {
            if (!ignoreEmptyFolders)
            {
                return !pathToCheck.GetFiles().Any() && !pathToCheck.GetDirectories().Any();
            }

            var finfos = pathToCheck.GetFiles("*", SearchOption.AllDirectories);
            return !finfos.Any();
        }

        /// <summary>
        ///     Checks if a directory contains any file or folder
        /// </summary>
        /// <param name="pathToCheck">The full directory path to check</param>
        /// <param name="ignoreEmptyFolders">When true this method will consider empty only directories with no files</param>
        /// <returns></returns>
        public static bool IsEmpty(string pathToCheck, bool ignoreEmptyFolders = false)
        {
            return new DirectoryInfo(pathToCheck).IsEmpty(ignoreEmptyFolders);
        }
    }
}