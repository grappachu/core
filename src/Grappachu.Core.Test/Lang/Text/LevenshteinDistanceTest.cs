using Grappachu.Core.Lang.Text;
using NUnit.Framework;

namespace Grappachu.Core.Test.Lang.Text
{
    [TestFixture]
    public class LevenshteinDistanceTest
    {
        [Test]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao Mondo!" }, ExpectedResult = 0)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao mondo!" }, ExpectedResult = 1)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao Tondo!" }, ExpectedResult = 1)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao Tondo?" }, ExpectedResult = 2)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao oMndo!" }, ExpectedResult = 2)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao World!" }, ExpectedResult = 4)]
        [TestCase(new object[] { "Ciao Mondo!", "CCiao Mondo" }, ExpectedResult = 2)]
        [TestCase(new object[] { "Ciao Mondo!", "Mondo Ciao!" }, ExpectedResult = 8)]
        public int Compute_test(string a, string b)
        {
            return LevenshteinDistance.Compute(a, b);
        }

        [Test]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao Mondo!" }, ExpectedResult = 0)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao mondo!" }, ExpectedResult = 0)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao Tondo!" }, ExpectedResult = 1)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao Tondo?" }, ExpectedResult = 2)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao oMndo!" }, ExpectedResult = 2)]
        [TestCase(new object[] { "Ciao Mondo!", "Ciao World!" }, ExpectedResult = 4)]
        [TestCase(new object[] { "Ciao Mondo!", "CCiao Mondo" }, ExpectedResult = 2)]
        [TestCase(new object[] { "Ciao Mondo!", "Mondo Ciao!" }, ExpectedResult = 8)]
        public int ComputeIgnoreCase_test(string a, string b)
        {
            return LevenshteinDistance.ComputeIgnoreCase(a, b);
        }
    }
}