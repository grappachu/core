using System.IO;
using Grappachu.Core.IO;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.IO
{
    public class DirectoryUtilsTest : XunitAbstractFilesystemTest
    {
        [Fact]
        public void IsEmpty_when_empty()
        {
            var dir = CreateDir("emptyFolder");
            var dinfo = new DirectoryInfo(dir);

            DirectoryUtils.IsEmpty(dir).Should().Be.True();
            dinfo.IsEmpty(true).Should().Be.True();
            dinfo.IsEmpty().Should().Be.True();
        }

        [Fact]
        public void IsEmpty_when_has_deep_nested_and_hidden_files_and_folders()
        {
            var dir = CreateDir("nested");
            CreateDir("nested\\lev1");
            var dir2 = CreateDir("nested\\lev1\\lev2");
            CreateDir("nested\\lev1\\lev2\\lev3");
            var hidden = CreateFile("nested\\lev1\\lev2\\lev3\\hidden");
            File.SetAttributes(dir, FileAttributes.Hidden);
            File.SetAttributes(dir2, FileAttributes.Hidden);
            File.SetAttributes(hidden, FileAttributes.Hidden);

            DirectoryUtils.IsEmpty(dir, true).Should().Be.False();
            DirectoryUtils.IsEmpty(dir).Should().Be.False();
        }

        [Fact]
        public void IsEmpty_when_has_files()
        {
            var dir = CreateDir("only_files");
            CreateFile("only_files\\file1");

            DirectoryUtils.IsEmpty(dir, true).Should().Be.False();
            DirectoryUtils.IsEmpty(dir).Should().Be.False();
        }


        [Fact]
        public void IsEmpty_when_has_hidden_files()
        {
            var dir = CreateDir("hidden_files");
            var file = CreateFile("hidden_files\\hiddenFile1");
            File.SetAttributes(file, FileAttributes.Hidden);

            DirectoryUtils.IsEmpty(dir, true).Should().Be.False();
            DirectoryUtils.IsEmpty(dir).Should().Be.False();
        }


        [Fact]
        public void IsEmpty_when_has_hidden_folders()
        {
            var dir = CreateDir("hidden_folders");
            var hidden = CreateDir("hidden_folders\\hidden_folder1");
            File.SetAttributes(hidden, FileAttributes.Hidden);

            DirectoryUtils.IsEmpty(dir, true).Should().Be.True();
            DirectoryUtils.IsEmpty(dir).Should().Be.False();
        }

        [Fact]
        public void IsEmpty_when_has_only_dirs()
        {
            var dir = CreateDir("only_folders");
            CreateDir("only_folders\\childDir1");

            DirectoryUtils.IsEmpty(dir, true).Should().Be.True();
            DirectoryUtils.IsEmpty(dir).Should().Be.False();
        }
    }
}