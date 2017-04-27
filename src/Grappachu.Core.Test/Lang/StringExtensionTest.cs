using Grappachu.Core.Lang.Extensions;
using NUnit.Framework;

namespace Grappachu.Core.Test.Lang
{
    [TestFixture]
    internal class StringExtensionsTest
    {
        [Test]
        public void ShouldConvertStringToDecimalIgnoringCulture()
        {
            var v1 = "123.4";
            var v2 = "123,4";
            var v3 = "1,234.5";
            var v4 = "1.234,5";
            var v5 = "123";
            var v6 = "1,234,567.89";
            var v7 = "1.234.567,89";

            var a1 = v1.ToDecimal();
            var a2 = v2.ToDecimal();
            var a3 = v3.ToDecimal();
            var a4 = v4.ToDecimal();
            var a5 = v5.ToDecimal();
            var a6 = v6.ToDecimal();
            var a7 = v7.ToDecimal();

            Assert.AreEqual((decimal) 123.4, a1);
            Assert.AreEqual((decimal) 123.4, a2);
            Assert.AreEqual((decimal) 1234.5, a3);
            Assert.AreEqual((decimal) 1234.5, a4);
            Assert.AreEqual((decimal) 123, a5);
            Assert.AreEqual((decimal) 1234567.89, a6);
            Assert.AreEqual((decimal) 1234567.89, a7);
        }
    }
}