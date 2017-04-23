using System;
using System.IO;
using Grappachu.Core.IO;
using Grappachu.Core.Runtime.Serialization;
using Grappachu.Core.Security.Hashing;
using Moq;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.IO
{
    [TestFixture]
    class FileManagerTest
    {
        private string _testFile, _testHashFile;

        [SetUp]
        public void SetUp()
        {
            _testFile = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testFile))
            {
                File.Delete(_testFile);
            }

            if (File.Exists(_testHashFile))
            {
                File.Delete(_testHashFile);
            }
        }


        #region SafeDelete

        [Test]
        public void SafeDelete_ShouldDeleteAFile()
        {
            var tempfile = Path.GetTempFileName();

            FileManager.SafeDelete(tempfile);

            File.Exists(tempfile).Should().Be.False();
        }

        [Test]
        public void SafeDelete_ShouldDoNothingWhenFileNotExist()
        {
            var tempfile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()); // Il file non esiste

            FileManager.SafeDelete(tempfile);

            File.Exists(tempfile).Should().Be.False();
        }

        #endregion

        #region GetSize

        [Test]
        public void GetSize_ShouldReturnFileSize()
        {
            const int datasize = 2347;
            var tempfile = Path.GetTempFileName();
            var data = new byte[datasize];
            File.WriteAllBytes(tempfile, data);

            var detectedSize = FileManager.GetSize(tempfile);

            detectedSize.Should().Be.EqualTo(datasize);
        }

        [Test]
        public void GetSize_ShouldReturnZeroWhenFileNotExist()
        {
            var tempfile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()); // Il file non esiste

            var detectedSize = FileManager.GetSize(tempfile);

            detectedSize.Should().Be.EqualTo(0);
        }

        #endregion

        #region SaveToFile

        [Test]
        public void SaveToFile_ShouldSaveAnObjectToAFile()
        {
            var objectToSave = new object();
            var serializer = new Mock<IRuntimeSerializer>();  ;
            var fileBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            serializer.Setup(x => x.Serialize(objectToSave)).Returns(fileBytes);

            var tempFile = Path.GetTempFileName();

            FileManager.SaveToFile(objectToSave, tempFile, serializer.Object);

            var writtenData = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);

            writtenData.Should().Have.SameSequenceAs(fileBytes);
        }

        #endregion

        #region  ReadFromFile

        [Test]
        public void ReadFromFile_ShouldRestoreAnObjectFromAFile()
        {
            const string objectToSave = "test object";
            var fileBytes = new byte[] { 0, 1, 0, 0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 11, 116, 101, 115, 116, 32, 111, 98, 106, 101, 99, 116, 11 };
            var serializer = new Mock<IRuntimeSerializer>();  
            serializer.Setup(x => x.Deserialize(fileBytes)).Returns(objectToSave);

            var tempFile = Path.GetTempFileName();
            File.WriteAllBytes(tempFile, fileBytes);

            var restored = FileManager.ReadFromFile<string>(tempFile, serializer.Object);


            File.Delete(tempFile);

            restored.Should().Be.EqualTo(objectToSave);
        }


        #endregion

        #region VerifyHash

        [Test]
        public void VerifyHash_ShouldReturnMissingWhenFileNotExist()
        {
            string inexistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "\\nofile.txt");

            var status = FileManager.VerifyHash(inexistentFile, It.IsAny<string>(),
                It.IsAny<IHashProvider>(), false);

            status.Should().Be.EqualTo(FileStatus.Missing);
        }

        [Test]
        public void VerifyHash_ShouldReturnCorruptedWhenHashNotMatch()
        {
            const string originalHash = "1234567890abcdefgh";
            const string calcualtedHash = "0987654321abcdefgh";
            var provider = new Mock<IHashProvider>();
            provider.Setup(x => x.HashFile(_testFile)).Returns(calcualtedHash);

            var status = FileManager.VerifyHash(_testFile, originalHash, provider.Object, false);

            status.Should().Be.EqualTo(FileStatus.Corrupted);
        }

        [Test]
        public void VerifyHash_ShouldReturnPresentWhenHashMatch()
        {
            const string originalHash = "1234567890abcdefgh";
            const string calcualtedHash = "1234567890abcdefgh";
            var provider = new Mock<IHashProvider>();
            provider.Setup(x => x.HashFile(_testFile)).Returns(calcualtedHash);

            var status = FileManager.VerifyHash(_testFile, originalHash, provider.Object, false);

            status.Should().Be.EqualTo(FileStatus.Present);
        }

        [Test]
        public void VerifyHash_ShouldReturnUnknownWhenErrorOccurs()
        {
            const string originalHash = "1234567890abcdefgh";
            var provider = new Mock<IHashProvider>();
            provider.Setup(x => x.HashFile(_testFile)).Throws(new AccessViolationException());

            var status = FileManager.VerifyHash(_testFile, originalHash, provider.Object, false);

            File.Delete(_testFile);

            status.Should().Be.EqualTo(FileStatus.Unknown);
        }


        [Test]
        public void VerifyHash_ShouldSaveCalcualtedHashInAFile()
        {
            var provider = new Mock<IHashProvider>();
            _testHashFile = _testFile + "." + provider.Object.Algorythm;
            const string originalHash = "1234567890abcdefgh";
            const string calcualtedHash = "1234567890abcdefgh";
            provider.Setup(x => x.HashFile(_testFile)).Returns(calcualtedHash);

            var status = FileManager.VerifyHash(_testFile, originalHash, provider.Object, true);

            status.Should().Be.EqualTo(FileStatus.Present);
            var hashfile = new FileInfo(_testHashFile);
            hashfile.Exists.Should().Be.True();
            hashfile.LastWriteTime.Should().Be.GreaterThanOrEqualTo(File.GetLastWriteTime(_testFile));
            hashfile.Attributes.Should().Be.EqualTo(FileAttributes.Hidden | FileAttributes.NotContentIndexed);
            File.ReadAllText(_testHashFile).Should().Be.EqualTo(calcualtedHash);
        }

        [Test]
        public void VerifyHash_ShouldUseSavedFileHashWhenAvailable()
        {
            var provider = new Mock<IHashProvider>();
            const string originalHash = "1234567890abcdefgh";
            _testHashFile = CreateHashFile(_testFile, provider.Object, originalHash);

            var status = FileManager.VerifyHash(_testFile, originalHash, provider.Object, true);

            status.Should().Be.EqualTo(FileStatus.Present);
            provider.Verify(x => x.HashFile(It.IsAny<string>()), Times.Never);
            File.GetLastWriteTime(_testHashFile)
                .Should().Be.GreaterThanOrEqualTo(File.GetLastWriteTime(_testFile));
        }



        [Test]
        public void VerifyHash_ShouldUpdateHashFileWhenIsOlderThanMainFile()
        {
            const string originalHash = "1234567890abcdefgh";
            // Expect to hash main file
            var provider = new Mock<IHashProvider>();
            provider.Setup(x => x.HashFile(_testFile)).Returns(originalHash);
            // Creates an old file hash 
            _testHashFile = CreateHashFile(_testFile, provider.Object, "-----old-hash------");
            File.SetLastWriteTime(_testHashFile, DateTime.Now.AddHours(-1));

            var status = FileManager.VerifyHash(_testFile, originalHash, provider.Object, true);


            status.Should().Be.EqualTo(FileStatus.Present);

            // Expects to overwrite old hash file
            File.GetLastWriteTime(_testHashFile)
                .Should().Be.GreaterThanOrEqualTo(File.GetLastWriteTime(_testFile));
            File.ReadAllText(_testHashFile).Should().Be.EqualTo(originalHash);

        }


        [Test]
        public void CleanHash_ShouldRemoveHashFile()
        {
            var provider = new Mock<IHashProvider>();
            _testHashFile = CreateHashFile(_testFile, provider.Object, "-----old-hash------");

            FileManager.CleanHash(_testFile, provider.Object);

            File.Exists(_testHashFile).Should().Be.False();
        }


        [Test]
        public void CleanHash_ShouldDoNothingIfHashFileDoNotExist()
        {
            var provider = new Mock<IHashProvider>();
            _testHashFile = _testFile + "." + provider.Object.Algorythm;

            FileManager.CleanHash(_testFile, provider.Object);

            File.Exists(_testHashFile).Should().Be.False();
        }


        private string CreateHashFile(string mainFile, IHashProvider provider, string content)
        {
            string hashFilePath = mainFile + "." + provider.Algorythm;
            File.WriteAllText(hashFilePath, content);
            File.SetAttributes(hashFilePath, FileAttributes.Hidden | FileAttributes.NotContentIndexed);
            return hashFilePath;
        }

        #endregion


    }
}
