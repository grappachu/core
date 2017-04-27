using System.Globalization;
using System.Linq;
using System.Threading;
using Grappachu.Core.Preview.Runtime.Threading;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Runtime.Threading
{
    [TestFixture]
    public class RuntimeHelperTests
    {
        public void RunWithCultureTest()
        {
            string cultureToRun = (new[] { "af-ZA", "tt-RU" })
                .First(c => c != CultureInfo.CurrentCulture.ToString());
            string res = null;

            RuntimeHelper.RunWithCulture(new CultureInfo(cultureToRun),
                () => { res = CultureInfo.CurrentCulture.ToString(); });

            res.Should().Be.EqualTo(cultureToRun);
            // Should not keep the culture in this thread
            CultureInfo.CurrentCulture.ToString().Should().Not.Be.EqualTo(cultureToRun);
        }

        [Test]
        public void RunAsStaThreadTest()
        {
            string res = null;
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                Assert.Inconclusive();
            }

            RuntimeHelper.RunAsStaThread(()
                => { res = Thread.CurrentThread.GetApartmentState().ToString(); });

            res.Should().Be.EqualTo(ApartmentState.STA.ToString());
            // Should not keep the value
            Thread.CurrentThread.GetApartmentState().Should().Not.Be.EqualTo(ApartmentState.STA);
        }
    }
}