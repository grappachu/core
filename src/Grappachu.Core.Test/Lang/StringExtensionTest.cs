using System;
using Grappachu.Core.Lang.Extensions;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Lang
{
    [TestFixture]
    internal class StringExtensionsTest
    {
        [TestCase("123.4", 123.4)]
        [TestCase("123,4", 123.4)]
        [TestCase("1,234.5", 1234.5)]
        [TestCase("1.234,5", 1234.5)]
        [TestCase("123", 123)]
        [TestCase("1,234,567.89", 1234567.89)]
        [TestCase("1.234.567,89", 1234567.89)]
        public void Should_Convert_String_ToDecimal_from_any_culture(string val, decimal expected)
        {
            var res = val.ToDecimal();

            Assert.AreEqual(expected, res);
        }



        [TestCase("123.4", 123.4)]
        [TestCase("123,4", 123.4)]
        [TestCase("1,234.5", 1234.5)]
        [TestCase("1.234,5", 1234.5)]
        [TestCase("123", 123)]
        [TestCase("1,234,567.89", 1234567.89)]
        [TestCase("1.234.567,89", 1234567.89)]
        public void ShouldConvertStringToDoubleIgnoringCulture(string val, double expected)
        {
            var res = val.ToDouble();

            Assert.AreEqual(expected, res);
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
            var expectedLines = (double)text.Length / lineLenght;

            var wrapped = text.Wrap(50);

            var lines = wrapped.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            lines.Length.Should().Be.IncludedIn(Math.Floor(expectedLines), Math.Ceiling(expectedLines));
        }
    }
}