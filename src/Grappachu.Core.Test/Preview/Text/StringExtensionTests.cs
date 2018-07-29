using System.Drawing;
using Grappachu.Core.Preview.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Preview.Text
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void ToBitmapTest()
        {
            const string text = "Cantami, o Diva, del Pelìde Achille l'ira funesta che \n" +
                                "infiniti addusse lutti agli Achei, molte anzi tempo \n" +
                                "all'Orco generose travolse alme d'eroi, e di cani \n" +
                                "e d'augelli orrido pasto lor salme abbandonò....";

            Bitmap image = text.ToBitmap(new Font("Arial", 10), Brushes.Red);

            image.Width.Should().Be.IncludedIn(250, 400);
            image.Height.Should().Be.IncludedIn(70, 80);

            image.Save("x:\\ttt.jpg");
        }

    
    }
}