using System.IO;
using Grappachu.Core.Collections;

namespace Grappachu.Core.IO
{
    /// <summary>
    ///     Defines a set of utility methods and extensions to work with files
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        ///     Creates a valid filename from a string by replacing invalida characters
        /// </summary>
        /// <param name="entryName">The string candidate to be a file name. This will be tipically the title of your object</param>
        /// <param name="extension">The extension for the filename to create</param>
        /// <param name="replaceChar">The char or string to be used as replacement</param>
        /// <param name="extraChars">Provides an extra list of characters that should be replaced</param>
        /// <returns></returns>
        public static string ToFilename(this string entryName, string extension, string replaceChar = "_",
            char[] extraChars = null)
        {
            var name = string.Join(replaceChar, entryName.Split(Path.GetInvalidFileNameChars()));
            if (!extraChars.IsNullOrEmpty())
            {
                name = string.Join(replaceChar, name.Split(extraChars));
            }

            return string.IsNullOrEmpty(extension) ? entryName : $"{name}.{extension}";
        }
        
    }
}