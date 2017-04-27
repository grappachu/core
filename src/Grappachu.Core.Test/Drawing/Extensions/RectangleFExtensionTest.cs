using System.Drawing;
using Grappachu.Core.Drawing.Extensions;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Drawing.Extensions
{
    [TestFixture]
    internal class RectangleFExtensionTest
    {
        [Test]
        public void GetCenterF_Should_ReturnCenter()
        {
            var shape = new RectangleF(120, 150, 640, 480);

            var center = shape.GetCenterF();

            center.X.Should().Be.IncludedIn(440, 440);
            center.Y.Should().Be.IncludedIn(390, 390);
        }

        [Test]
        public void GetCenter_Should_ReturnCenter()
        {
            var shape = new RectangleF(120, 150, 640, 480);

            var center = shape.GetCenter();

            center.X.Should().Be.EqualTo(440);
            center.Y.Should().Be.EqualTo(390);
        }


        [TestCase(10f, 640f, 9f, ExpectedResult = false)]
        [TestCase(10f, 640f, 10f, ExpectedResult = true)]
        [TestCase(10f, 640f, 300f, ExpectedResult = true)]
        [TestCase(10f, 640f, 650f, ExpectedResult = true)]
        [TestCase(10f, 640f, 651f, ExpectedResult = false)]
        public bool IntersectX_Should_Return_True_WhenXIsInsideShape(float left, float width, float x)
        {
            var shape = new RectangleF(left, 10, width, 10);

            var res = shape.IntersectX(x);

            return res;
        }


        [TestCase(10f, 480, 9f, ExpectedResult = false)]
        [TestCase(10f, 480, 10f, ExpectedResult = true)]
        [TestCase(10f, 480, 245f, ExpectedResult = true)]
        [TestCase(10f, 480, 490, ExpectedResult = true)]
        [TestCase(10f, 480, 491, ExpectedResult = false)]
        public bool IntersectY_Should_Return_True_WhenXIsInsideShape(float top, float height, float y)
        {
            var shape = new RectangleF(10, top, 10, height);

            var res = shape.IntersectY(y);

            return res;
        }


    }
}