using System;
using System.IO;

namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    ///     Represent a temporary file
    /// </summary>
    public sealed class TempFile : IDisposable
    {
        private const string DefaultExtension = "tmp";
        private readonly string _path;

        /// <summary>
        ///     Generates a new Temporary file reference in the temp folder
        /// </summary>
        public TempFile()
            : this(DefaultExtension)
        {
        }

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
            _path = System.IO.Path.Combine(root, System.IO.Path
                .ChangeExtension(Guid.NewGuid().ToString(), extension));
        }

        /// <summary>
        ///     Obtains the name of the temporary file to use
        /// </summary>
        public string Path
        {
            get { return _path; }
        }


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
                if (_path != null)
                {
                    if (File.Exists(_path))
                    {
                        File.Delete(_path);
                    }
                }
            }
        }

        /// <summary>
        ///     Writes the empty file on disk
        /// </summary>
        public void Create()
        {
            using (File.Create(_path))
            {
            }
        }
    }
}