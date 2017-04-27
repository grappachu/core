using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Grappachu.Core.Preview.IO;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.IO
{
    [TestFixture]
    internal class FileEnumeratorTest
    {
        [SetUp]
        public void SetUp()
        {
            _testRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testRoot);

            CreateFiles(_testRoot);
            for (int i = 1; i <= 5; i++)
            {
                string testDir = Path.Combine(_testRoot, i.ToString(CultureInfo.InvariantCulture));
                Directory.CreateDirectory(testDir);
                CreateFiles(testDir);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_testRoot))
            {
                Directory.Delete(_testRoot, true);
            }
        }

        private string _testRoot;
        private FileEnumerator _sut;

        private void CreateFiles(string root)
        {
            for (int i = 1; i <= 5; i++)
            {
                string testfile = Path.Combine(root, i + ".test");
                File.WriteAllText(testfile, Guid.NewGuid().ToString());
            }
        }


        [Test]
        public void ShouldEnumerateAllFilesInADirectory()
        {
            _sut = new FileEnumerator();
            _sut.Paths.Add(new PathSearchInfo(_testRoot));

            FileInfo[] list = _sut.ToArray();

            list.Length.Should().Be(5);
            list.Select(x => x.Name)
                .Should()
                .Have.SameValuesAs(new[] {"1.test", "2.test", "3.test", "4.test", "5.test"});
        }

        [Test]
        public void ShouldEnumerateAllFilesInADirectoryRecursively()
        {
            _sut = new FileEnumerator();
            _sut.Paths.Add(new PathSearchInfo(_testRoot, SearchOption.AllDirectories));

            FileInfo[] list = _sut.ToArray();

            list.Length.Should().Be(5 + (5*5));
        }

        [Test]
        public void ShouldEnumerateAllFilesMatchingAFilter()
        {
            _sut = new FileEnumerator();
            _sut.Paths.Add(new PathSearchInfo(_testRoot, "3.*", SearchOption.AllDirectories));

            FileInfo[] list = _sut.ToArray();

            list.Length.Should().Be(1 + (5));
            list.Select(x => x.Name).All(x => x.StartsWith("3.")).Should().Be.True();
        }


        [Test]
        public void ShouldEnumerateFilesFromMultiplePaths()
        {
            _sut = new FileEnumerator();
            _sut.Paths.Add(new PathSearchInfo(Path.Combine(_testRoot, "2")));
            _sut.Paths.Add(new PathSearchInfo(Path.Combine(_testRoot, "4")));

            FileInfo[] list = _sut.ToArray();

            list.Length.Should().Be(2*5);
        }


        [Test]
        public void ShouldNotReturnDuplicatedItems()
        {
            _sut = new FileEnumerator();
            _sut.Paths.Add(new PathSearchInfo(_testRoot, "3.test", SearchOption.AllDirectories));
            _sut.Paths.Add(new PathSearchInfo(Path.Combine(_testRoot, "4")));

            FileInfo[] list = _sut.ToArray();

            list.Length.Should().Be((1 + 5) + (5) - 1);
        }
    }
}