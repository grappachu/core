using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using System.Data;
using System.Drawing;
using Grappachu.Core.Util.Layout;

namespace Grappachu.Core.Test.Util.Layout
{
    [TestFixture]
    internal class TableLayoutEngineTest
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new TableLayoutEngine(new Font("Arial", 10, FontStyle.Regular));

            _sampleData = new DataTable("TestData");
            _sampleData.Columns.Add("C1");
            _sampleData.Columns.Add("C2");
            _sampleData.Columns.Add("C3");
            _sampleData.Columns.Add("C4");
            _sampleData.Columns.Add("C5");
            _sampleData.Columns.Add("C6");
        }

        private TableLayoutEngine _sut;

        private DataTable _sampleData;

        [Test]
        public void GetColumnSizes_Should_AllowToCustomizeFont()
        {
            const int TOTAL_SPACE = 1000;
            _sampleData.Rows.Add(new object[]
            {"Sample Column", ",Sample Column", "Sample Column", "Sample Column", "Sample Column", "Sample Column"});

            _sut.MeasuringCell += (sender, args) =>
            {
                switch (args.Column)
                {
                    case 1:
                        args.CellFont = new Font("Arial", 10, FontStyle.Bold);
                        break;
                    case 2:
                        args.CellFont = new Font("Arial", 12, FontStyle.Bold);
                        break;
                }
            };

            int[] res = _sut.GetColumnSizes(_sampleData, TOTAL_SPACE);


            foreach (int re in res)
            {
                Console.WriteLine(re);
            }
            res[1].Should().Be.GreaterThan(res[0]);
            res[2].Should().Be.GreaterThan(res[0]);
            res[3].Should().Be.EqualTo(res[0]);
            res[4].Should().Be.EqualTo(res[0]);
            res[5].Should().Be.EqualTo(res[0]);
        }

        [Test]
        public void GetColumnSizes_Should_AllowToCustomizePadding()
        {
            const int TOTAL_SPACE = 1000;
            _sampleData.Rows.Add(new object[]
            {"Sample Column", ",Sample Column", "Sample Column", "Sample Column", "Sample Column", "Sample Column"});

            _sut.MeasuringCell += (sender, args) =>
            {
                switch (args.Column)
                {
                    case 1:
                        args.CellPadding = 5;
                        break;
                    case 2:
                        args.CellPadding = 15;
                        break;
                }
            };

            int[] res = _sut.GetColumnSizes(_sampleData, TOTAL_SPACE);


            foreach (int re in res)
            {
                Console.WriteLine(re);
            }
            res[1].Should().Be.GreaterThan(res[0]);
            res[2].Should().Be.GreaterThan(res[0]);
            res[3].Should().Be.EqualTo(res[0]);
            res[4].Should().Be.EqualTo(res[0]);
            res[5].Should().Be.EqualTo(res[0]);
        }

        [Test]
        public void GetColumnSizes_Should_ConsiderWrapping()
        {
            const int TOTAL_SPACE = 50;
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "long column that should be wrapped", "", "zz1"});
            _sampleData.Rows.Add(new object[]
            {"1", ",bbb", "c", "long column that should be wrapped, this is really long", "", "zz"});
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "this is short", "", "ZZ2"});


            int[] res = _sut.GetColumnSizes(_sampleData, TOTAL_SPACE);

            foreach (int re in res)
            {
                Console.WriteLine(re);
            }

            // column 3 will be wrapped
            res[3].Should().Be.LessThan(res[1]*3);
        }

        [Test]
        public void GetColumnSizes_Should_DistributeWidthsProperly()
        {
            const int TOTAL_SPACE = 1000;
            _sampleData.Rows.Add(new object[] {"Zero", "One", ",Two-Two", "Three", "Four is longer than three", "Five is less"});

            int[] res = _sut.GetColumnSizes(_sampleData, TOTAL_SPACE);

            foreach (int re in res)
            {
                Console.WriteLine(re);
            }

            res[0].Should().Be.GreaterThan(res[1]);
            res[1].Should().Be.LessThan(res[2]);
            res[2].Should().Be.GreaterThan(res[3]);
            res[4].Should().Be.GreaterThan(res[2]);
            res[5].Should().Be.LessThan(res[4]);
            res[5].Should().Be.GreaterThan(res[2]);
        }

        [Test]
        public void GetColumnSizes_Should_FitColumnsToTableWidth()
        {
            const int TOTAL_SPACE = 500;
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "ddd ddd ddd", "ee", "zz"});
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "ddd ddd ddd ddd ddd ddd dd", "ee", "zz"});
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "ddd ddd ddd", "ee", "zz"});

            int[] res = _sut.GetColumnSizes(_sampleData, TOTAL_SPACE);

            foreach (int re in res)
            {
                Console.WriteLine(re);
            }

            int epsilon = _sampleData.Columns.Count;
            res.Sum().Should().Be.IncludedIn(TOTAL_SPACE - epsilon, TOTAL_SPACE + epsilon);
        }

        [Test]
        public void GetColumnSizes_Should_UseCustomValues()
        {
            const int TOTAL_SPACE = 480;
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "ddd ddd ddd", "ee", "zz"});
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "ddd ddd dddd d", "ee", "zz"});
            _sampleData.Rows.Add(new object[] {"1", ",bbb", "c", "ddd ddd ddd", "ee", "zz"});

            _sut.MeasuringCell += (sender, args) =>
            {
                args.MaxTextWidth = 80;
                args.MaxWordWidth = 80;
            };

            int[] res = _sut.GetColumnSizes(_sampleData, TOTAL_SPACE);

            res.Should().Have.SameSequenceAs(new[] {80, 80, 80, 80, 80, 80});
        }
    }
}