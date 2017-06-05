using System;
using Grappachu.Core.Lang.Extensions;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Lang
{
    [TestFixture]
    internal class StringExtensionsTest
    {
        [Test]
        public void ShouldConvertStringToDecimalIgnoringCulture()
        {
            var v1 = "123.4";
            var v2 = "123,4";
            var v3 = "1,234.5";
            var v4 = "1.234,5";
            var v5 = "123";
            var v6 = "1,234,567.89";
            var v7 = "1.234.567,89";

            var a1 = v1.ToDecimal();
            var a2 = v2.ToDecimal();
            var a3 = v3.ToDecimal();
            var a4 = v4.ToDecimal();
            var a5 = v5.ToDecimal();
            var a6 = v6.ToDecimal();
            var a7 = v7.ToDecimal();

            Assert.AreEqual((decimal) 123.4, a1);
            Assert.AreEqual((decimal) 123.4, a2);
            Assert.AreEqual((decimal) 1234.5, a3);
            Assert.AreEqual((decimal) 1234.5, a4);
            Assert.AreEqual((decimal) 123, a5);
            Assert.AreEqual((decimal) 1234567.89, a6);
            Assert.AreEqual((decimal) 1234567.89, a7);
        }


        [Test]
        public void Wrap_should_add_newlines_in_a_text()
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
            var expectedLines = (double) text.Length / lineLenght;

            var wrapped = text.Wrap(50);

            var lines = wrapped.Split(new[] {System.Environment.NewLine}, StringSplitOptions.None);
            lines.Length.Should().Be.IncludedIn(Math.Floor(expectedLines), Math.Ceiling(expectedLines));
        }
    }
}