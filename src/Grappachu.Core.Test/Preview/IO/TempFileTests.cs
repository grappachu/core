using System;
using System.IO;
using Grappachu.Core.Preview.IO;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Preview.IO
{
    [TestFixture]
    public class TempFileTests : GenericFolderBasedTest
    {
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


        [Test]
        public void CreateTest()
        {
            using (var t = new TempFile(ToString()))
            {
                t.Create();
            }
        }

        [Test]
        public void Create_should_write_file_on_disk()
        {
            using (var t = new TempFile(TestRoot, "tmp"))
            {
                t.Create();
                File.Exists(t.Path).Should().Be.True();
            }
        }

        [Test]
        public void Dispose_should_delete_file()
        {
            string path;
            using (var t = new TempFile(ToString()))
            {
                path = t.Path;
                t.Create();
            }

            File.Exists(path).Should().Be.False();
        }

        [Test]
        public void TempFile_constructor_should_generate_a_file_path_with_specified_extension()
        {
            string ext = "." + Guid.NewGuid().ToString().Substring(0, 3);
            using (var t = new TempFile(TestRoot, ext))
            {
                File.Exists(t.Path).Should().Be.False();
                Path.GetExtension(t.Path).Should().Be.EqualTo(ext);
            }
        }

        [Test]
        public void TempFile_constructor_should_use_temp_path_by_default()
        {
            using (var t = new TempFile())
            {
                t.Path.StartsWith(Path.GetTempPath()).Should().Be.True();
            }
        }
    }
}