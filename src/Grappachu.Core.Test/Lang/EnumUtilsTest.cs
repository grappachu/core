using System.ComponentModel;
using Grappachu.Core.Lang;
using Grappachu.Core.Lang.Extensions;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.Lang
{
    public class EnumUtilsTest
    {
        public enum TestEnum
        {
            Val1,
            Val2,
            [Description("Value 3")] Val3
        }

        [Theory(DisplayName = "Should Parse strings into enums")]
        [InlineData("Val2", TestEnum.Val2)]
        public void Parse_should_return_an_enum(string input, TestEnum output)
        {
            var res = EnumHelper<TestEnum>.Parse(input);

            res.Should().Be(output);
        }


        [Fact]
        public void Should_get_all_Values()
        {
            var res = EnumHelper<TestEnum>.GetValues();

            res.Should().Have.SameSequenceAs(new[] {TestEnum.Val1, TestEnum.Val2, TestEnum.Val3});
        }


        [Fact]
        public void Should_get_description_from_a_value()
        {
            var res = TestEnum.Val3.GetDescription();

            res.Should().Be("Value 3");
        }
    }
}