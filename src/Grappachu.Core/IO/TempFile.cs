using System;
using System.IO;

namespace Grappachu.Core.IO
{
    /// <inheritdoc />
    /// <summary>
    ///     Represent a temporary file path that will be deleted when disposed
    /// </summary>
    /// <example>
    /// <code>
    /// using(var tmp = new TempFile("jpg"))
    /// {
    ///      // write your file
    ///      GeneratePicture(someData, tmp.Path);
    ///
    ///      // do something with your file
    ///      SendPictureByEmail(tmp.Path);
    /// }
    ///   // file has been removed from disk, you don't mind it
    /// </code>
    /// </example>
    public sealed class TempFile : IDisposable
    {
        private const string DEFAULT_EXTENSION = "tmp";

        /// <inheritdoc />
        /// <summary>
        ///     Generates a new Temporary file reference in the temp folder
        /// </summary>
        public TempFile()
            : this(DEFAULT_EXTENSION)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Generates a new Temporary file reference in the temp folder whith the specified extension
        /// </summary>
        public TempFile(string extension)
            : this(System.IO.Path.GetTempPath(), extension)
        {
        }

        /// <summary>
        ///     Generates a new Temporary file reference in the provided folder using the specified extension
        /// </summary>
        public TempFile(string root, string extension)
        {
            Path = System.IO.Path.Combine(root, System.IO.Path
                .ChangeExtension(Guid.NewGuid().ToString(), extension));
        }

        /// <summary>
        ///     Obtains the full path of the temporary file to use
        /// </summary>
        public string Path { get; }


        /// <inheritdoc />
        /// <summary>Esegue attività definite dall'applicazione, come rilasciare o reimpostare risorse non gestite.</summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (Path != null)
                {
                    if (File.Exists(Path))
                    {
                        File.Delete(Path);
                    }
                }
            }
        }

        /// <summary>
        ///     Writes the empty file on disk
        /// </summary>
        public void Create()
        {
            using (File.Create(Path))
            {
            }
        }
    }
}