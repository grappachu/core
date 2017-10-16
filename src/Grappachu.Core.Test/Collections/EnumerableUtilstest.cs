using Grappachu.Core.Collections;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Collections
{
    [TestFixture]
    public class EnumerableUtilsTest
    {
        [Test]
        public void IsNullOrEmpty_should_be_false_only_when_array_has_items()
        {
            object[] noArray = null;
            object[] emptyArray = new object[0];
            object[] fullArray = { "ciao" };

            noArray.IsNullOrEmpty().Should().Be.True();
            emptyArray.IsNullOrEmpty().Should().Be.True();
            fullArray.IsNullOrEmpty().Should().Be.False();
        }
    }
}
