using System;
using System.IO;
using Grappachu.Core.Preview.IO;

namespace Grappachu.Core.Test.IO.Abstract
{
    public abstract class GenericFolderBasedTest
    {
        private string _testRoot;

        protected string TestRoot { get { return _testRoot; } }

        #region Protected Methods

        protected string CreateFile(string path, bool makeReadonly = false)
        {
            var target = Path.IsPathRooted(path) ? path : Path.Combine(_testRoot, path);
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
                FilesystemTools.RemoveReadonly(_testRoot, true);
                Directory.Delete(_testRoot, true);
            }
        }



        #endregion

    }
}