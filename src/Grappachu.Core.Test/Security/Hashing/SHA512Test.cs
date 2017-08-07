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
    internal class SHA512Test
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new SHA512();
        }

        private SHA512 _sut;

        [Test]
        public void ShouldHashFilesAsBase64()
        {
            var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
            const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
            File.WriteAllText(tempFile, testString);

            var result = _sut.Hash(new FileInfo(tempFile)).ToString(BinaryRepresentation.Base64);

            result.Should().Be.EqualTo("9hqt0jVUaA+FixoayRIm2SWM644Meqw/gpwjij/5pFM4wtA8E/umbQ1bG52YMc8K+RQ5VLzuJvzDXvzClHvfyw==");

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

            result.Should().Be.EqualTo("f61aadd23554680f858b1a1ac91226d9258ceb8e0c7aac3f829c238a3ff9a45338c2d03c13fba66d0d5b1b9d9831cf0af9143954bcee26fcc35efcc2947bdfcb");

            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }

        [Test]
        public void ShouldHashStringsAsBase64()
        {
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
            const string expectedResult = "5YDeMXoyYWLTJKniEQjljxM8nU2a52hP+eHjf5fIFsZKz/XUkqM0/8cyRcLe3YF58jNOlKglKCaHpgk+bZmTqg==";

            var result = _sut.Hash(testString).ToString(BinaryRepresentation.Base64);

            result.Should().Be.EqualTo(expectedResult);
        }


        [Test]
        public void ShouldHashStringsUsingMd5Algorythm()
        {
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
            const string expectedResult = "e580de317a326162d324a9e21108e58f133c9d4d9ae7684ff9e1e37f97c816c64acff5d492a334ffc73245c2dedd8179f2334e94a825282687a6093e6d9993aa";

            var result = _sut.Hash(testString).ToString(BinaryRepresentation.Hex);

            result.Should().Be.EqualTo(expectedResult);
        }

        [Test]
        public void ShouldImplementIHashProvider()
        {
            var hashProvider = _sut as IHashProvider;

            hashProvider.Should().Not.Be.Null();
        }

        [Test]
        public void Should_expose_static_methods_for_hashing_strings()
        {
            const string testData = "Cantami o diva del pelide Achille l'ira funesta";

            SHA512.HashString(testData).Should().Have.SameSequenceAs(_sut.Hash(testData));
        }

        [Test]
        public void Should_expose_static_methods_for_hashing_files()
        {
            var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
            const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
            File.WriteAllText(tempFile, testString);
            
            SHA512.HashString(tempFile).Should().Have.SameSequenceAs(_sut.Hash(tempFile));

            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);

        }
    }
}