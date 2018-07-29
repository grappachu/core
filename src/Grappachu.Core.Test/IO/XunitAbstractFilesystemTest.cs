using System;
using System.IO;

namespace Grappachu.Core.Test.IO
{
    /// <summary>
    /// Defines a base class for filesystem based tests
    /// </summary>
    public abstract class XunitAbstractFilesystemTest : IDisposable
    {
        private string _testRoot;

        protected string TestRoot => _testRoot;

        protected XunitAbstractFilesystemTest()
        {
            OnSetUp();
        }

        #region Protected Methods

        protected string CreateFile(string relativeOrAbsoluteFilePath, bool makeReadonly = false)
        {
            var target = Path.IsPathRooted(relativeOrAbsoluteFilePath) 
                ? relativeOrAbsoluteFilePath 
                : Path.Combine(_testRoot, relativeOrAbsoluteFilePath);
            File.WriteAllText(target, @"Test Content");
            if (makeReadonly)
            {
                File.SetAttributes(target, FileAttributes.ReadOnly);
            }
            return target;
        }

        protected string CreateDir(string path, bool makeReadonly = false)
        {
            var target = Path.IsPathRooted(path) ? path : Path.Combine(_testRoot, path);
            Directory.CreateDirectory(target);
            if (makeReadonly)
            {
                File.SetAttributes(target, FileAttributes.ReadOnly);
            }
            return target;
        }

        #endregion

        #region Init

        protected void OnSetUp()
        {
            _testRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testRoot);
        }



        protected void OnTearDown()
        {
            System.Threading.Thread.Sleep(100); // Timeout for ensure windows release files
            if (Directory.Exists(_testRoot))
            {
                foreach (var f in Directory.GetFileSystemEntries(_testRoot))
                {
                    var fatt = File.GetAttributes(f);
                    if (File.GetAttributes(f).HasFlag(FileAttributes.ReadOnly))
                    {
                        FileAttributes modAttr = fatt & ~FileAttributes.ReadOnly;
                        File.SetAttributes(f, modAttr);
                    }
                }
                RemoveReadonly(_testRoot, true);
                Directory.Delete(_testRoot, true);
            }
        }



        private static bool IsDirectory(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        private static void RemoveReadonly(string targetPath, bool recursive = false)
        {
            FileAttributes attr = File.GetAttributes(targetPath);
            if (attr.HasFlag(FileAttributes.ReadOnly))
            {
                FileAttributes modAttr = attr & ~FileAttributes.ReadOnly;
                File.SetAttributes(targetPath, modAttr);
            }

            if (IsDirectory(targetPath) && recursive)
            {
                string[] items = Directory.GetFileSystemEntries(targetPath, "*", SearchOption.AllDirectories);
                foreach (string item in items)
                {
                    RemoveReadonly(item);
                }
            }
        }

        #endregion


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnTearDown();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}