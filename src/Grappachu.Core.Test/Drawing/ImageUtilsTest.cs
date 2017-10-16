using System.Drawing;
using System.Drawing.Imaging;
using Grappachu.Core.Drawing;
using Grappachu.Core.Drawing.Extensions;
using Grappachu.Core.Preview.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Drawing
{
    [TestFixture]
    public class ImageUtilsTest
    {
        [Test]
        public void GetImage_should_load_image_from_a_byte_array()
        {
            var img = "Test image".ToBitmap(new Font("Arial", 10));
            var imgBytes = img.GetBytes();

            var loaded = ImageUtils.GetImage(imgBytes);

            loaded.Size.Should().Be.EqualTo(img.Size);
        }


        [Test]
        public void ToBase64_Should_convert_an_image_to_base64_string()
        {
            var img = "Test image".ToBitmap(new Font("Arial", 10));

            var base64 = img.ToBase64(ImageFormat.Png);

            base64.Length.Should().Be(892);
        }
    }
}