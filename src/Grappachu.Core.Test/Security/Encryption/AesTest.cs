using System;
using Grappachu.Core.Lang;
using Grappachu.Core.Lang.Text;
using Grappachu.Core.Preview.Encryption;
using Grappachu.Core.Security;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Security.Encryption
{
    [TestFixture]
    internal class AesTest
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new AES();
        }

        private AES _sut;

        //[Test]
        //public void ShouldEncryptFilesAsBase64()
        //{
        //    var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
        //    const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
        //    const string expectedResult = "9YCcj/qhIvCff9/89SWwBw==";
        //    File.WriteAllText(tempFile, testString);
        //    _sut.OutputFormat = OutputEncoding.Base64;

        //    var result = _sut.HashFile(tempFile);

        //    result.Should().Be.EqualTo(expectedResult);

        //    // Cleanup
        //    if (File.Exists(tempFile))
        //    {
        //        File.Delete(tempFile);
        //    }
        //}


        //[Test]
        //public void ShouldEncryptFilesUsingAesAlgorythm()
        //{
        //    var tempFile = string.Format("{0}{1}.dat", Path.GetTempPath(), Guid.NewGuid());
        //    const string testString = "My test content to hash with special chars 123!|\"£$%&/()=<>{}[]";
        //    const string expectedResult = "f5809c8ffaa122f09f7fdffcf525b007";
        //    File.WriteAllText(tempFile, testString);

        //    var result = _sut.HashFile(tempFile);

        //    result.Should().Be.EqualTo(expectedResult);

        //    // Cleanup
        //    if (File.Exists(tempFile))
        //    {
        //        File.Delete(tempFile);
        //    }
        //}

        [Test]
        public void ShouldEncryptAndDecryptStringsAsBase64()
        {
            const string testKey = "SecretKey123";
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
             _sut.OutputFormat = BinaryRepresentation.Base64;

            var encrypted = _sut.Encrypt(testString, testKey);
            Console.WriteLine(@"Encrypted value: {0}", encrypted);

            var result = _sut.Decrypt(encrypted, testKey);
            Console.WriteLine(@"Decrypted value: {0}", result);

            result.Should().Be.EqualTo(testString); 
            encrypted.Should().Not.Be.EqualTo(testString);
        }


        [Test]
        public void ShouldEncryptStringsUsingAesAlgorythm()
        {
            const string testKey = "SecretKey123";
            const string testString = "Cantami o diva del pelide Achille l'ira funesta";
             _sut.OutputFormat = BinaryRepresentation.Hex;

            var encrypted  = _sut.Encrypt(testString, testKey);
            Console.WriteLine(@"Encrypted value: {0}", encrypted);

            var result = _sut.Decrypt(encrypted, testKey);
            Console.WriteLine(@"Decrypted value: {0}", result);

            result.Should().Be.EqualTo(testString);
            encrypted.Should().Not.Be.EqualTo(testString);
        }

        [Test]
        public void ShouldImplementIEncryptionProvider()
        {
            var provider = _sut as IEncryptionProvider;

            provider.Should().Not.Be.Null();
        }
    }
}