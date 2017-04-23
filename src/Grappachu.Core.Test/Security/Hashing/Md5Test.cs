using System;
using System.IO;
using Grappachu.Core.Security;
using Grappachu.Core.Security.Hashing;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Security.Hashing
{
    [TestFixture]
    class Md5Test
    {

        private MD5 _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MD5();
        }

        [Test]
        public void ShouldImplementIHashProvider()
        {
            var hashProvider = _sut as IHashProvider;

            hashProvider.Should().Not.Be.Null();
        }


        [Test]
        public void ShouldHashStringsUsingMd5Algorythm()
        {
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
            const string expectedResult = "b4dd7f0b0ca6c25dd46cc096e45158eb";

            string result = _sut.HashString(testString);

            result.Should().Be.EqualTo(expectedResult);
        }

        [Test]
        public void ShouldHashStringsAsBase64()
        {
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
            const string expectedResult = "tN1/Cwymwl3UbMCW5FFY6w==";
            _sut.OutputFormat = OutputEncoding.Base64;

            string result = _sut.HashString(testString);

            result.Should().Be.EqualTo(expectedResult);
        }



        [Test]
        public void ShouldHashFilesUsingMd5Algorythm()
        {
            var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
            const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
            const string expectedResult = "f5809c8ffaa122f09f7fdffcf525b007";
            File.WriteAllText(tempFile, testString);

            string result = _sut.HashFile(tempFile);

            result.Should().Be.EqualTo(expectedResult);

            // Cleanup
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }

        [Test]
        public void ShouldHashFilesAsBase64()
        {
            var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
            const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
            const string expectedResult = "9YCcj/qhIvCff9/89SWwBw==";
            File.WriteAllText(tempFile, testString);
            _sut.OutputFormat = OutputEncoding.Base64;

            string result = _sut.HashFile(tempFile);

            result.Should().Be.EqualTo(expectedResult);

            // Cleanup
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }

    }

   
}
