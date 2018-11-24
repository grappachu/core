using Grappachu.Core.IO;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.IO
{
    public class FileUtilsTest : XunitAbstractFilesystemTest
    {
        [Theory]
        [InlineData("This is: a valid/invalid filename?", "txt", "This_is__a_valid_invalid_filename_.txt")]
        public void EscapeName_should_get_a_valid_filename(string title, string extension, string expected)
        {
            var res = title.ToFilename(extension, "_", new[] {' '});

            res.Should().Be.EqualTo(expected);
        }
    }
}