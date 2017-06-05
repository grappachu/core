using System.Drawing;
using Grappachu.Core.Drawing;
using Grappachu.Core.Drawing.Extensions;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Drawing.Extensions
{
    [TestFixture]
    internal class ImageExtensionsTest
    {
        [Test]
        public void EnlargeCanvas_should_add_border_or_specified_thickness()
        {
            Image imgSrc = new Bitmap(200, 100);

            var imgOut = imgSrc.EnlargeCanvas(5, Color.Black);

            imgOut.Width.Should().Be.EqualTo(210);
            imgOut.Height.Should().Be.EqualTo(110);
        }


        [Test]
        public void EnlargeCanvas_should_add_border_to_fit_output_size()
        {
            Image imgSrc = new Bitmap(200, 100);

            var imgOut = imgSrc.EnlargeCanvas(new Size(300, 300), Color.Black);

            imgOut.Width.Should().Be.EqualTo(300);
            imgOut.Height.Should().Be.EqualTo(300);
        }

        [Test]
        public void GetGraphics_should_create_graphics_from_image()
        {
            Image imgSrc = new Bitmap(200, 100);

            var g = imgSrc.GetGraphics(GraphicsQuality.Highest);

            g.Should().Not.Be.Null();
        }

        [Test]
        public void Resize_should_create_a_stretched_image()
        {
            Image imgSrc = new Bitmap(200, 100);

            var imgOut = imgSrc.Resize(new Size(300, 300));

            imgOut.Width.Should().Be.EqualTo(300);
            imgOut.Height.Should().Be.EqualTo(300);
        }

        [Test]
        public void ScaleToFill_should_enlarge_and_crop_to_output_size()
        {
            Image imgSrc = new Bitmap(200, 100);

            var imgOut = imgSrc.ScaleToFill(new Size(300, 300));

            imgOut.Width.Should().Be.EqualTo(300);
            imgOut.Height.Should().Be.EqualTo(300);
        }

        [Test]
        public void ScaleToFit_should_resize_image_proportionally()
        {
            Image imgSrc = new Bitmap(200, 100);

            var imgOut = imgSrc.ScaleToFit(new Size(300, 300));

            imgOut.Width.Should().Be.EqualTo(300);
            imgOut.Height.Should().Be.EqualTo(150);
        }

        [Test]
        public void ScaleToSize_should_scale_and_add_borders_to_match_output_size()
        {
            Image imgSrc = new Bitmap(200, 100);

            var imgOut = imgSrc.ScaleToSize(new Size(300, 300), Color.Black);

            imgOut.Width.Should().Be.EqualTo(300);
            imgOut.Height.Should().Be.EqualTo(300);
        }


        //[Test]
        //public void ToBitmap_should_cast_a_bitmap()
        //{
        //    Image imgSrc = new Bitmap(200, 100);

        //    var bmp = imgSrc.ToBitmap();

        //    bmp.Should().Be.SameInstanceAs(imgSrc);
        //}


        [Test]
        public void ToByteArray_should_return_image_bytes()
        {
            Image imgSrc = new Bitmap(200, 100);

            var bytes = imgSrc.GetBytes();

            bytes.Length.Should().Be.EqualTo(208);
        }

        //    System.IO.File.WriteAllBytes("x:\\img1.png", imgSrc.GetBytes(ImageFormat.Png));
        //    System.IO.File.WriteAllBytes("x:\\img1.bmp", imgSrc.GetBytes(ImageFormat.Bmp));

        //    System.IO.File.WriteAllBytes("x:\\img1.jpg", imgSrc.GetBytes(ImageFormat.Jpeg));

        //    System.IO.File.WriteAllBytes("x:\\img0.jpg", imgSrc.GetBytes());
        //   Image imgSrc = new Bitmap(2200, 1200, PixelFormat.Format24bppRgb);
        //{
        //public void perf2()

        //[Test]
        //    System.IO.File.WriteAllBytes("x:\\img1.gif", imgSrc.GetBytes(ImageFormat.Gif));
        //}
    }
}