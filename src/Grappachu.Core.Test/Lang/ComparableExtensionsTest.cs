using System;
using Grappachu.Core.Drawing;
using Grappachu.Core.Lang.Extensions;
using NUnit.Framework;
using SharpTestsEx;
using Xunit;

namespace Grappachu.Core.Test.Lang
{
    [TestFixture]
    public class ComparableExtensionsTest
    {
        [Test]
        [TestCase(new object[] { 2, 3, 6 }, ExpectedResult = false)]
        [TestCase(new object[] { 3, 3, 6 }, ExpectedResult = true)]
        [TestCase(new object[] { 5, 3, 6 }, ExpectedResult = true)]
        [TestCase(new object[] { 6, 3, 6 }, ExpectedResult = true)]
        [TestCase(new object[] { 7, 3, 6 }, ExpectedResult = false)]
        [TestCase(new object[] { "AD", "AE", "BC" }, ExpectedResult = false)]
        [TestCase(new object[] { "BB", "AE", "BC" }, ExpectedResult = true)]
        [TestCase(new object[] { "BC", "AE", "BC" }, ExpectedResult = true)]
        [TestCase(new object[] { "CC", "AE", "BC" }, ExpectedResult = false)]
        public bool Between_test(IComparable val, IComparable low, IComparable high)
        {
            return val.Between(low, high);
        }


        [Test]
        [TestCase(new object[] { 2, 3, 6 }, ExpectedResult = false)]
        [TestCase(new object[] { 3, 3, 6 }, ExpectedResult = false)]
        [TestCase(new object[] { 5, 3, 6 }, ExpectedResult = true)]
        [TestCase(new object[] { 6, 3, 6 }, ExpectedResult = false)]
        [TestCase(new object[] { 7, 3, 6 }, ExpectedResult = false)]
        [TestCase(new object[] { "AD", "AE", "BC" }, ExpectedResult = false)]
        [TestCase(new object[] { "BB", "AE", "BC" }, ExpectedResult = true)]
        [TestCase(new object[] { "BC", "AE", "BC" }, ExpectedResult = false)]
        [TestCase(new object[] { "CC", "AE", "BC" }, ExpectedResult = false)]
        public bool Inside_test(IComparable val, IComparable low, IComparable high)
        {
            return val.BetweenStrict(low, high);
        }


        [Test]
        [TestCase(new object[] { null, "X" }, ExpectedResult = "X")]
        [TestCase(new object[] { "J", "X" }, ExpectedResult = "J")]
        [TestCase(new object[] { default(string), "X" }, ExpectedResult = "X")]
        [TestCase(new object[] { default(bool), true }, ExpectedResult = false)]
        [TestCase(new object[] { default(RotationDirection), RotationDirection.AntiClockwise }, ExpectedResult = RotationDirection.Clockwise)]
        public IComparable Or_test(IComparable pref, IComparable alt)
        {
            return pref.Or(alt);
        }


        [Xunit.Theory]
        [InlineData(null, true)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        public void OrDie_should_throw_new_exception(string param, bool expectThrows)
        {
            const string message = "no value provided";
            Func<string> getFunc = () => param;

            string res = null;
            var ex = Record.Exception(() =>
            {
                res = getFunc.Invoke().OrDie(message);
            });

            if (expectThrows)
            {
                res.Should().Be.Null();
                ex.Should().Be.OfType<NullReferenceException>();
                ex?.Message.Should().Be.EqualTo(message);
            }
            else
            {
                res.Should().Be.EqualTo(param);
            }
        }
    }
}