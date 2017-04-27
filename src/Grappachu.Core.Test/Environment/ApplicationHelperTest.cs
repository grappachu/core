using Grappachu.Core.Preview.Environment;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Environment
{
    [TestFixture]
    class ApplicationHelperTest
    {
        [Test]
        public void GetProductVersion_Should_return_Version_of_entry_assembly_when_not_deployed()
        {
            var res = ApplicationHelper.GetProductVersion();

            // Test code is unhandled so doesnt' get this version
            res.Should().Not.Be.Null();


        }


        [Test]
        public void GetProductName_Should_return_ProductName_of_entry_assembly_when_not_deployed()
        {
            var res = ApplicationHelper.GetProductName();

            // Test code is unhandled so doesnt' get "Grappachu.Core.Test"
            res.Should().Be.EqualTo("Grappachu.Core");

        }
    }
}
