using Grappachu.Core.Lang;
using NUnit.Framework;

namespace Grappachu.Core.Test.Lang
{
    [TestFixture]
    internal class StringExtensionsTest
    {
        [Test]
        public void ShouldConvertStringToDecimalIgnoringCulture()
        {
            string v1 = "123.4";
            string v2 = "123,4";
            string v3 = "1,234.5";
            string v4 = "1.234,5";
            string v5 = "123";
            string v6 = "1,234,567.89";
            string v7 = "1.234.567,89";

            decimal a1 = v1.ToDecimal();
            decimal a2 = v2.ToDecimal();
            decimal a3 = v3.ToDecimal();
            decimal a4 = v4.ToDecimal();
            decimal a5 = v5.ToDecimal();
            decimal a6 = v6.ToDecimal();
            decimal a7 = v7.ToDecimal();

            Assert.AreEqual((decimal)123.4, a1);
            Assert.AreEqual((decimal)123.4, a2);
            Assert.AreEqual((decimal)1234.5, a3);
            Assert.AreEqual((decimal)1234.5, a4);
            Assert.AreEqual((decimal)123, a5);
            Assert.AreEqual((decimal)1234567.89, a6);
            Assert.AreEqual((decimal)1234567.89, a7);
        }


    }
}