using Grappachu.Core.Collections;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.Lang
{
    public class LinqUtilsTest  
    {
        [Fact]
        public void TakeLast_should_get_last_items()
        {
            var data = new[] {1, 2, 3, 4, 5, 6, 8, 7};

            var last = data.TakeLast(3);

            last.Should().Have.SameSequenceAs(new[] {6, 8, 7});
        }
    }
}