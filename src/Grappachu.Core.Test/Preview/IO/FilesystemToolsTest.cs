using System.IO;
using Grappachu.Core.IO;
using Grappachu.Core.Preview.IO;
using Grappachu.Core.Test.IO.Abstract;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Preview.IO
{
    [TestFixture]
    internal class FilesystemToolsTest : GenericFolderBasedTest
    {
        #region Init

        [SetUp]
        public void SetUp()
        {
            OnSetUp();
        }

        [TearDown]
        public void TearDown()
        {
            OnTearDown();
        }

        #endregion

      
        #region Exists

        [Test]
        public void Exists_should_check_files()
        {
            using (var tmp = new TempFile())
            {
                tmp.Create();

                FilesystemTools.Exists(tmp.Path).Should().Be.True();
            }
        }

        [Test]
        public void Exists_should_check_directories()
        {
            var dir = CreateDir(@"\TestDir");

            var dirExist = FilesystemTools.Exists(dir);

            dirExist.Should().Be.True();
        }

        [Test]
        public void Exists_should_check_missing_items()
        {
            var missing = FilesystemTools.Exists(Path.Combine(TestRoot, "nothing"));

            missing.Should().Be.False();
        }

        #endregion

        #region IsDirectory

        [Test]
        public void IsDirectory_should_be_True_for_directories()
        {
            var dir = CreateDir(@"\TestDir");

            var dirIsDir = FilesystemTools.IsDirectory(dir);

            dirIsDir.Should().Be.True();
        }

        [Test]
        public void IsDirectory_should_be_False_for_files()
        {
            using (var tmp = new TempFile())
            {
                tmp.Create();

                FilesystemTools.IsDirectory(tmp.Path).Should().Be.False();
            }
        }

        [Test]
        public void IsDirectory_should_Throw_when_entry_missing()
        {
            Executing.This(() => FilesystemTools.IsDirectory(Path.Combine(TestRoot, "nothing")))
                  .Should().Throw<FileNotFoundException>();
        }

        #endregion

        [Test]
        public void IsReadonly_should_check_a_file()
        {
            var roFile = CreateFile(@"ROFile", true);
            var noRoFole = CreateFile(@"noRoFole");

            var r1 = FilesystemTools.IsReadonly(roFile, false);
            var r2 = FilesystemTools.IsReadonly(noRoFole, false);

            r1.Should().Be.True();
            r2.Should().Be.False();
        }

        [Test]
        public void IsReadonly_should_check_any_file_in_a_folder_when_specified()
        {
            var dirWithRoFile = CreateDir(@"Dir1");
            CreateFile(@"Dir1\RoFile", true);

            var dirWithoutRoFiles = CreateDir(@"Dir2");
            CreateFile(@"Dir2\noRoFile");

            var dirRo = CreateDir(@"Dir3", true);
            CreateFile(@"Dir3\noRoFile");

            var r1A = FilesystemTools.IsReadonly(dirWithRoFile, false);
            var r1B = FilesystemTools.IsReadonly(dirWithRoFile, true);
            var r2A = FilesystemTools.IsReadonly(dirWithoutRoFiles, false);
            var r2B = FilesystemTools.IsReadonly(dirWithoutRoFiles, true);
            var r3A = FilesystemTools.IsReadonly(dirRo, false);
            var r3B = FilesystemTools.IsReadonly(dirRo, true);

            r1A.Should().Be.False();
            r1B.Should().Be.True();
            r2A.Should().Be.False();
            r2B.Should().Be.False();
            r3A.Should().Be.True();
            r3B.Should().Be.True();
        }


        [Test]
        public void RemoveReadonlyTest()
        {
            var dir1 = CreateDir(@"Dir1");
            var f1 = CreateFile(@"Dir1\RoFile", true);
            var d1 = CreateDir(@"Dir1\RoDir", true);

            var dir2 = CreateDir(@"Dir2", true);
            var f2 = CreateFile(@"Dir2\RoFile", true);
            var d2 = CreateDir(@"Dir2\RoDir", true);

            FilesystemTools.RemoveReadonly(dir1, true);

            FilesystemTools.RemoveReadonly(dir2);

            File.GetAttributes(f1).HasFlag(FileAttributes.ReadOnly).Should().Be.False();
            File.GetAttributes(d1).HasFlag(FileAttributes.ReadOnly).Should().Be.False();
            File.GetAttributes(dir2).HasFlag(FileAttributes.ReadOnly).Should().Be.False();
            File.GetAttributes(f2).HasFlag(FileAttributes.ReadOnly).Should().Be.True();
            File.GetAttributes(d2).HasFlag(FileAttributes.ReadOnly).Should().Be.True();
        }

        #region Rename

        [Test]
        public void Rename_should_move_files()
        {
            var file = CreateFile(@"testFile");
            var targetFile = Path.Combine(TestRoot, "movedFile");

            FilesystemTools.Rename(file, targetFile);

            File.Exists(targetFile).Should().Be.True();
            File.Exists(file).Should().Be.False();
        }

        [Test]
        public void Rename_should_move_directories()
        {
            var dir = CreateDir(@"TestDir");
            CreateDir(@"TestDir\subDir");
            CreateFile(@"TestDir\subDir\file.dat");
            var targetDir = Path.Combine(TestRoot, "MovedDir");

            FilesystemTools.Rename(dir, targetDir);

            Directory.Exists(targetDir).Should().Be.True();
            Directory.Exists(dir).Should().Be.False();
            File.Exists(Path.Combine(targetDir, @"subDir\file.dat")).Should().Be.True();
        }

        [Test]
        public void Rename_should_Throw_when_entry_missing()
        {
            var missingEntry = Path.Combine(TestRoot, "missingEntry");
            var targetFile = Path.Combine(TestRoot, "movedFile");

            Executing.This(()
                => FilesystemTools.Rename(missingEntry, targetFile))
                .Should().Throw<FileNotFoundException>();
        }

        #endregion

        [Test]
        public void SafeCreateDirectoryTest()
        {
            var dir = CreateDir(@"testDir");
            var dirToCreate = Path.Combine(TestRoot, "dirToCreate");

            Executing.This(()
                 => FilesystemTools.SafeCreateDirectory(dir)).Should().NotThrow();

            FilesystemTools.SafeCreateDirectory(dirToCreate);

            Directory.Exists(dir).Should().Be.True();
            Directory.Exists(dirToCreate).Should().Be.True();
        }


        [Test]
        public void SafeDelete_should_delete_files()
        {
            using (var tmp = new TempFile())
            {
                tmp.Create();

                FilesystemTools.SafeDelete(tmp.Path);

                File.Exists(tmp.Path).Should().Be.False();
            }
        }

        [Test]
        public void SafeDelete_should_delete_readonly_files_when_specified()
        {
            var file = CreateFile(@"testFile", true);

            Executing.This(() => FilesystemTools.SafeDelete(file)).Should().Throw();
            Executing.This(() => FilesystemTools.SafeDelete(file, false, true)).Should().NotThrow();

            File.Exists(file).Should().Be.False();
        }


        [Test]
        public void SafeDelete_should_delete_readonly_files_recursively()
        {
            var file = CreateFile(@"testFile");
            var dir = CreateDir(@"TestDir");
            CreateDir(@"TestDir\SubDir");
            CreateFile(@"TestDir\SubDir\testFile");

            FilesystemTools.SafeDelete(file);
            File.Exists(file).Should().Be.False();

            Executing.This(() => FilesystemTools.SafeDelete(dir)).Should().Throw();
            FilesystemTools.SafeDelete(dir, true);
            Directory.Exists(dir).Should().Be.False();

            Executing.This(()
                => FilesystemTools.SafeDelete(Path.Combine(TestRoot, "nothing")))
                .Should().NotThrow();
        }

        [Test]
        public void SafeDelete_should_delete_directories_recursively_when_specified()
        {
            var dir = CreateDir(@"\TestDir");
            CreateDir(@"\TestDir\SubDir");
            CreateFile(@"\TestDir\SubDir\testFile");

            Executing.This(() => FilesystemTools.SafeDelete(dir)).Should().Throw();
            FilesystemTools.SafeDelete(dir, true);
            Directory.Exists(dir).Should().Be.False();
        }


        [Test]
        public void SafeDelete_should_delete_directories_with_readonly_files()
        {
            var dir = CreateDir(@"\TestDir");
            CreateDir(@"\TestDir\SubDir");
            CreateFile(@"\TestDir\SubDir\testFile", true);

            Executing.This(() => FilesystemTools.SafeDelete(dir, true)).Should().Throw();
            FilesystemTools.SafeDelete(dir, true, true);
            Directory.Exists(dir).Should().Be.False();
        }


    }
}