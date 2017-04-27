using System;
using System.IO;
using Grappachu.Core.Lang.Extensions;
using Grappachu.Core.Lang.Text;
using Grappachu.Core.Security.Hashing;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Security.Hashing
{
    [TestFixture]
    internal class Md5Test
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new MD5();
        }

        private MD5 _sut;

        [Test]
        public void ShouldHashFilesAsBase64()
        {
            var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
            const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
            File.WriteAllText(tempFile, testString);

            var result = _sut.Hash(new FileInfo(tempFile)).ToString(BinaryRepresentation.Base64);

            result.Should().Be.EqualTo("9YCcj/qhIvCff9/89SWwBw==");

            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }


        [Test]
        public void ShouldHashFilesUsingMd5Algorythm()
        {
            var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
            const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
            File.WriteAllText(tempFile, testString);

            var result = _sut.Hash(new FileInfo(tempFile)).ToString(BinaryRepresentation.Hex);

            result.Should().Be.EqualTo("f5809c8ffaa122f09f7fdffcf525b007");

            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }

        [Test]
        public void ShouldHashStringsAsBase64()
        {
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
            const string expectedResult = "tN1/Cwymwl3UbMCW5FFY6w==";

            var result = _sut.Hash(testString).ToString(BinaryRepresentation.Base64);

            result.Should().Be.EqualTo(expectedResult);
        }


        [Test]
        public void ShouldHashStringsUsingMd5Algorythm()
        {
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
            const string expectedResult = "b4dd7f0b0ca6c25dd46cc096e45158eb";

            var result = _sut.Hash(testString).ToString(BinaryRepresentation.Hex);

            result.Should().Be.EqualTo(expectedResult);
        }

        [Test]
        public void ShouldImplementIHashProvider()
        {
            var hashProvider = _sut as IHashProvider;

            hashProvider.Should().Not.Be.Null();
        }
    }
}