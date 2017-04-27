using System.Globalization;
using Grappachu.Core.Globalization;
using Grappachu.Core.Globalization.Providers;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Globalization.Providers
{
    [TestFixture]
    public class FileSizeFormatterTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        private readonly CultureInfo _italian = new CultureInfo("it-IT");
        private readonly CultureInfo _english = new CultureInfo("en-GB");

        [Test]
        public void ShouldFormatFileSizeProperly()
        {
            const long bytes = 123456;
            const string expectedIt = "120,56 Kb";
            const string expectedEn = "120.56 Kb";

            var fIta = string.Format(new FileSizeFormatter(_italian), "{0}", bytes);
            var fEng = string.Format(new FileSizeFormatter(_english), "{0:g}", bytes);

            fIta.Should().Be.EqualTo(expectedIt);
            fEng.Should().Be.EqualTo(expectedEn);
        }


        [Test]
        public void ShouldUseInvariantCultureCultureByDefault()
        {
            const long bytes = 123456;

            var resInvariant = bytes.ToString(new FileSizeFormatter(CultureInfo.InvariantCulture));
            var resDefault = bytes.ToString(new FileSizeFormatter());

            resInvariant.Should().Be.EqualTo(resDefault);
        }
    }
}