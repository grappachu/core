using System.IO;
using Grappachu.Core.IO;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.IO
{
    public class PathUtilsTest : XunitAbstractFilesystemTest
    {
        [Fact]
        public void SuggestFilename_should_numerate_files_while_file_exist()
        {
            File.WriteAllText(Path.Combine(TestRoot, "testfile.txt"), "a");
            File.WriteAllText(Path.Combine(TestRoot, "testfile (2).txt"), "b");
            File.WriteAllText(Path.Combine(TestRoot, "testfile (3).txt"), "c");

            var fname = PathUtils.SafeCombine(TestRoot, "testfile.txt", "{0} ({1}).{2}", 2);

            fname.Should().Be.EqualTo("testfile (4).txt");
        }



        [Fact]
        public void ClonePathTest()
        {
            var source = CreateDir(@"sourceDir");
            var child1 = CreateFile(@"sourceDir\child1");
            CreateDir(@"sourceDir\subDir");
            var subChild1 = CreateFile(@"sourceDir\subDir\subChild1");
            var subChild2 = CreateFile(@"sourceDir\subDir\subChild2");

            var target = CreateDir("targetDir");
            var childToErase = CreateFile(@"targetDir\childToErase");

            PathUtils.Clone(source, target);

            File.Exists(childToErase).Should().Be.False();
            File.Exists(child1).Should().Be.True();
            File.Exists(subChild1).Should().Be.True();
            File.Exists(subChild2).Should().Be.True();
        }

    }
}