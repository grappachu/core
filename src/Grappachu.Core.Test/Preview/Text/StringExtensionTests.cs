using System;
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
            image.Height.Should().Be.IncludedIn(50, 70);
        }

        [Test]
        public void WrapTest()
        {
            const int lineLenght = 50;
            const string text = "Cantami, o Diva, del Pelìde Achille l'ira funesta che infiniti addusse lutti " +
                                "agli Achei, molte anzi tempo all'Orco generose travolse alme d'eroi, e di cani " +
                                "e d'augelli orrido pasto lor salme abbandonò (così di Giove l'alto consiglio " +
                                "s'adempìa), da quando primamente disgiunse aspra contesa il re de' prodi Atride " +
                                "e il divo Achille. E qual de' numi inimicolli? Il figlio di Latona e di Giove. " +
                                "Irato al Sire destò quel Dio nel campo un feral morbo, e la gente perìa: colpa " +
                                "d'Atride che fece a Crise sacerdote oltraggio. Degli Achivi era Crise alle veloci " +
                                "prore venuto a riscattar la figlia con molto prezzo. In man le bende avea, e " +
                                "l'aureo scettro dell'arciero Apollo: e agli Achei tutti supplicando, e in prima " +
                                "ai due supremi condottieri Atridi: O Atridi, ei disse, o coturnati Achei, " +
                                "gl'immortali del cielo abitatori concedanvi espugnar la Prïameia cittade, e " +
                                "salvi al patrio suol tornarvi. Deh mi sciogliete la diletta figlia, ricevetene " +
                                "il prezzo, e il saettante figlio di Giove rispettate.";
            double expectedLines = (double) text.Length/lineLenght;

            string wrapped = text.Wrap(50);

            string[] lines = wrapped.Split(new[] {System.Environment.NewLine}, StringSplitOptions.None);
            lines.Length.Should().Be.IncludedIn(Math.Floor(expectedLines), Math.Ceiling(expectedLines));
        }
    }
}