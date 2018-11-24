using System;
using Grappachu.Core.Lang;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.Lang
{
    public class StringUtilsTest
    {
        [Theory]
        [InlineData("Address: Baker Street, 54 - London", ":", ",", " Baker Street")]
        [InlineData("ma la volpe col suo balzo", "la", "suo", " volpe col ")]
        [InlineData("Go down Moses, Way down in Egypt land", " ", " ", "down")]
        public void Extract_should_get_text_between_content(string input, string before, string after, string expected)
        {
            var result = input.Extract(before, after);

            result.Should().Be.EqualTo(expected);
        }

        [Theory]
        [InlineData("AB[5]f[gFS[[5T]EaD]S[AV5]SCFSADSGACEF", "[", "]", 10, "[5T")]
        [InlineData("AB[5]f[gFS[[5T]EaD]S[AV5]SCFSADSGACEF", "[", "]", 11, "5T")]
        public void Extract_should_get_text_between_content_defining_start(string input, string before, string after, int startIndex, string expected)
        {
            var result = input.Extract(before, after, startIndex);

            result.Should().Be.EqualTo(expected);
        }


        [Theory]
        [InlineData("abcdefghiABEDCFGHI", "B", "F", "cde")]
        public void Extract_should_get_text_allowing_ignore_case(string input, string before, string after, string expected)
        {
            var result = input.Extract(before, after, StringComparison.InvariantCultureIgnoreCase);

            result.Should().Be.EqualTo(expected);
        }

        
        [Theory]
        [InlineData("abcdefghiABEDCFGHI", "efg", true)]
        [InlineData("abcdefghiABEDCFGHI", "eFg", true)]
        public void Contains_should_look_for_text_ignoring_case(string input, string toFind, bool expected)
        {
            var result = input.ContainsIgnoreCase(toFind);

            result.Should().Be.EqualTo(expected );
        }

    }
}