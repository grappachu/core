using System;
using System.IO;
using Grappachu.Core.Lang.Extensions;
using Grappachu.Core.Preview.IO;

namespace Grappachu.Core.IO
{
    /// <summary>
    ///     Defines a set of utility methods and extensions to manipulating filesystem paths
    /// </summary>
    public static class PathUtils
    {
        /// <summary>
        ///     Creates a relative filename based on <paramref name="desiredFilename" /> which actually doesn't exist in the
        ///     <paramref name="targetDirectory" /> path by adding some numbers at the end.
        /// </summary>
        /// <param name="targetDirectory">The firectory where the file should be saved</param>
        /// <param name="desiredFilename">The desired filename for the object</param>
        /// <param name="template">The custom template used to new generated files. Placehoders are {0} = filename, {1} = generated number, {2} = extension.</param>
        /// <param name="countStart">The start number for numerating</param>
        /// <returns>A new relative filename for the file</returns>
        /// <example>
        /// <code>
        ///  var productTitle = "My awesome product!";
        ///  var candidateName = productTitle.ToFilename("htm")
        /// 
        ///  var relativePath1 = PathUtils.SafeCombine( "C:\\Products", candidateName);
        ///  SaveProduct(relativePath1)
        ///  
        ///  var relativePath2 =  PathUtils.SafeCombine( "C:\\Products",candidateName);
        ///  SaveProduct(relativePath2)
        ///  // path 2 will be different from path 1 
        /// </code>
        /// </example>
        [Obsolete("Use PathUtils.GetSafePath()")]
        public static string SafeCombine(string targetDirectory, string desiredFilename,
            string template = "{0} ({1}).{2}", int countStart = 1)
        {
            var root = targetDirectory;
            var fname = Path.GetFileNameWithoutExtension(desiredFilename);
            var ext = Path.GetExtension(desiredFilename).Or(String.Empty).Trim('.');

            var candidate = desiredFilename;
            var idx = countStart - 1;
            while (FilesystemTools.Exists(Path.Combine(root, candidate)))
            {
                idx++;
                candidate = String.Format(template, fname, idx, ext);
            }

            return candidate;
        }

        /// <summary>
        ///     Creates a full path based on <paramref name="desiredFilename" /> which actually doesn't exist in the
        ///     <paramref name="targetDirectory" /> path by adding some numbers at the end when needed.
        /// </summary>
        /// <param name="targetDirectory">The directory where the file should be saved</param>
        /// <param name="desiredFilename">The desired filename for the object</param>
        /// <param name="template">The custom template used to new generated files. Placehoders are {0} = file name without extension, {1} = generated number, {2} = file extension.</param>
        /// <param name="countStart">The start number for numerating</param>
        /// <returns>A new relative filename for the file</returns>
        /// <example>
        /// <code>
        ///  var productTitle = "My awesome product!";
        ///  var candidateName = productTitle.ToFilename("htm")
        /// 
        /// // candidateName will be like: My_awesome_product_.htm
        /// 
        ///  var fullPath1 = PathUtils.SafeCombine( "C:\\Products", candidateName);
        ///  SaveProduct(fullPath1)
        ///  
        /// // path 1 will be like: C:\Products\My_awesome_product_.htm
        /// 
        ///  var fullPath2 =  PathUtils.SafeCombine( "C:\\Products",candidateName);
        ///  SaveProduct(fullPath1)
        ///  
        /// // path 2 will be like: C:\Products\My_awesome_product_(1).htm
        ///  
        /// </code>
        /// </example>
        public static string GetSafePath(string targetDirectory, string desiredFilename,
            string template = "{0}({1}).{2}", int countStart = 1)
        {
            var root = targetDirectory;
            var fname = Path.GetFileNameWithoutExtension(desiredFilename);
            var ext = Path.GetExtension(desiredFilename).Or(String.Empty).Trim('.');

            var candidate = desiredFilename;
            var idx = countStart - 1;
            while (FilesystemTools.Exists(Path.Combine(root, candidate)))
            {
                idx++;
                candidate = String.Format(template, fname, idx, ext);
            }

            return candidate;
        }




        /// <summary>
        ///     Clones a file or a directory tree into a new path.
        ///     All files in the target will be erased.
        /// </summary>
        public static void Clone(string sourcePath, string targetPath)
        {
            if (FilesystemTools.IsDirectory(sourcePath))
            {
                if (FilesystemTools.Exists(targetPath))
                {
                    FilesystemTools.SafeDelete(targetPath, true, true);
                }
                Directory.CreateDirectory(targetPath);

                // Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                    SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                    SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }
            else
            {
                File.Copy(sourcePath, targetPath, true);
            }
        }
    }
}