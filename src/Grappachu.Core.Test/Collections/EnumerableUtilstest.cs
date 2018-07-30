using System.Collections.Generic;
using System.Linq;
using Grappachu.Core.Collections;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.Collections
{
    public class EnumerableUtilsTest
    {
        [Fact]
        public void IsNullOrEmpty_should_be_false_only_when_array_has_items()
        {
            object[] noArray = null;
            object[] emptyArray = new object[0];
            object[] fullArray = { "ciao" };

            noArray.IsNullOrEmpty().Should().Be.True();
            emptyArray.IsNullOrEmpty().Should().Be.True();
            fullArray.IsNullOrEmpty().Should().Be.False();
        }



        [Fact]
        public void Split_should_divide_a_list_inot_chunks()
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            var chunked = data.ToChunks(5).ToArray();

            chunked.Count().Should().Be(4);
            chunked.ElementAt(1).Should().Have.SameSequenceAs(new[] { 6, 7, 8, 9, 10 });
            chunked.ElementAt(3).Should().Have.SameSequenceAs(new[] { 16, 17 });
        }
    }
}
