using System.Globalization;
using Grappachu.Core.Globalization;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Globalization
{
    [TestFixture]
    class StringHelperTest
    {
        private  StringHelper _sutIt;
        private StringHelper _sutEn;

        private readonly CultureInfo _italian = new CultureInfo("it-IT");
        private readonly CultureInfo _english = new CultureInfo("en-GB");

        [SetUp]
        public void SetUp()
        {
            _sutIt = new StringHelper(_italian);
            _sutEn = new StringHelper(_english);
        }


        [Test]
        public void ShouldUseInvariantCultureByDefault()
        {
            var sut = new StringHelper();

            sut.Culture.Should().Be.EqualTo(CultureInfo.InvariantCulture);
        }

        [Test]
        public void ShouldFormatFileSizeProperly()
        {
            const long bytes = 123456;
            const string expectedIt = "120,56 Kb"; 
            const string expectedEn = "120.56 Kb";  

            var resIt = _sutIt.FormatFileSize(bytes);
            var resEn = _sutEn.FormatFileSize(bytes);

            resIt.Should().Be.EqualTo(expectedIt);
            resEn.Should().Be.EqualTo(expectedEn);
        }

        [Test]
        public void ShouldExposeCulture()
        {
            var res = _sutIt.Culture;

            res.Should().Be.EqualTo(_italian);
        }


    }
}
