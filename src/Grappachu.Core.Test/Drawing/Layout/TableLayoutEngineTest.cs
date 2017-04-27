using System;
using System.Data;
using System.Drawing;
using System.Linq;
using Grappachu.Core.Drawing.Layout;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Drawing.Layout
{
    [TestFixture]
    internal class TableLayoutEngineTest : IDisposable
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
            const int totalSpace = 1000;
            _sampleData.Rows.Add("Sample Column", ",Sample Column", "Sample Column", "Sample Column", "Sample Column",
                "Sample Column");

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

            var res = _sut.GetColumnSizes(_sampleData, totalSpace);


            foreach (var re in res)
                Console.WriteLine(re);
            res[1].Should().Be.GreaterThan(res[0]);
            res[2].Should().Be.GreaterThan(res[0]);
            res[3].Should().Be.EqualTo(res[0]);
            res[4].Should().Be.EqualTo(res[0]);
            res[5].Should().Be.EqualTo(res[0]);
        }

        [Test]
        public void GetColumnSizes_Should_AllowToCustomizePadding()
        {
            const int totalSpace = 1000;
            _sampleData.Rows.Add("Sample Column", ",Sample Column", "Sample Column", "Sample Column", "Sample Column",
                "Sample Column");

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

            var res = _sut.GetColumnSizes(_sampleData, totalSpace);


            foreach (var re in res)
                Console.WriteLine(re);
            res[1].Should().Be.GreaterThan(res[0]);
            res[2].Should().Be.GreaterThan(res[0]);
            res[3].Should().Be.EqualTo(res[0]);
            res[4].Should().Be.EqualTo(res[0]);
            res[5].Should().Be.EqualTo(res[0]);
        }

        [Test]
        public void GetColumnSizes_Should_ConsiderWrapping()
        {
            const int totalSpace = 50;
            _sampleData.Rows.Add("1", ",bbb", "c", "long column that should be wrapped", "", "zz1");
            _sampleData.Rows.Add("1", ",bbb", "c", "long column that should be wrapped, this is really long", "", "zz");
            _sampleData.Rows.Add("1", ",bbb", "c", "this is short", "", "ZZ2");


            var res = _sut.GetColumnSizes(_sampleData, totalSpace);

            foreach (var re in res)
                Console.WriteLine(re);

            // column 3 will be wrapped
            res[3].Should().Be.LessThan(res[1] * 3);
        }

        [Test]
        public void GetColumnSizes_Should_DistributeWidthsProperly()
        {
            const int totalSpace = 1000;
            _sampleData.Rows.Add("Zero", "One", ",Two-Two", "Three", "Four is longer than three", "Five is less");

            var res = _sut.GetColumnSizes(_sampleData, totalSpace);

            foreach (var re in res)
                Console.WriteLine(re);

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
            const int totalSpace = 500;
            _sampleData.Rows.Add("1", ",bbb", "c", "ddd ddd ddd", "ee", "zz");
            _sampleData.Rows.Add("1", ",bbb", "c", "ddd ddd ddd ddd ddd ddd dd", "ee", "zz");
            _sampleData.Rows.Add("1", ",bbb", "c", "ddd ddd ddd", "ee", "zz");

            var res = _sut.GetColumnSizes(_sampleData, totalSpace);

            foreach (var re in res)
                Console.WriteLine(re);

            var epsilon = _sampleData.Columns.Count;
            res.Sum().Should().Be.IncludedIn(totalSpace - epsilon, totalSpace + epsilon);
        }

        [Test]
        public void GetColumnSizes_Should_UseCustomValues()
        {
            const int totalSpace = 480;
            _sampleData.Rows.Add("1", ",bbb", "c", "ddd ddd ddd", "ee", "zz");
            _sampleData.Rows.Add("1", ",bbb", "c", "ddd ddd dddd d", "ee", "zz");
            _sampleData.Rows.Add("1", ",bbb", "c", "ddd ddd ddd", "ee", "zz");

            _sut.MeasuringCell += (sender, args) =>
            {
                args.MaxTextWidth = 80;
                args.MaxWordWidth = 80;
            };

            var res = _sut.GetColumnSizes(_sampleData, totalSpace);

            res.Should().Have.SameSequenceAs(new[] {80, 80, 80, 80, 80, 80});
        }

        public void Dispose()
        {
            _sampleData.Dispose();
        }
    }
}