using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Grappachu.Core.Preview.IO;
using Moq;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.IO
{
    [TestFixture]
    internal class FileIndexTest : IDisposable
    {
        [SetUp]
        public void SetUp()
        {
            _keyGenerator = new Mock<IKeyGenerator<string, string>>();

            _f1 = Path.GetTempFileName();
            _f2 = Path.GetTempFileName();
            _f3 = Path.GetTempFileName();
            _f4 = Path.GetTempFileName();

            File.WriteAllText(_f1, Guid.NewGuid().ToString());
            File.WriteAllText(_f2, Guid.NewGuid().ToString());
            File.WriteAllText(_f3, Guid.NewGuid().ToString());
            File.WriteAllText(_f4, Guid.NewGuid().ToString());


            _enumerator =
                new EnumeratorMock(new[] { new FileInfo(_f1), new FileInfo(_f2), new FileInfo(_f3), new FileInfo(_f4) });

            _keyGenerator.Setup(x => x.GenerateKey(_f1)).Returns("A");
            _keyGenerator.Setup(x => x.GenerateKey(_f2)).Returns("B");
            _keyGenerator.Setup(x => x.GenerateKey(_f3)).Returns("A");
            _keyGenerator.Setup(x => x.GenerateKey(_f4)).Returns("C");


            _sut = new FileIndex(_keyGenerator.Object, _enumerator);
        }

        [TearDown]
        public void TearDown()
        {
            FileManager.SafeDelete(_f1);
            FileManager.SafeDelete(_f2);
            FileManager.SafeDelete(_f3);
            FileManager.SafeDelete(_f4);
        }

        private string _f1;
        private string _f2;
        private string _f3;
        private string _f4;

        private class EnumeratorMock : List<FileInfo>, IFileEnumerator
        {
            public EnumeratorMock(IEnumerable<FileInfo> fileInfos)
            {
                AddRange(fileInfos);
            }
        }

        private FileIndex _sut;


        private Mock<IKeyGenerator<string, string>> _keyGenerator;
        private IFileEnumerator _enumerator;

        [Test]
        public void GetFiles_ShouldReturnEmptyListIfNoMatch()
        {
            string[] res = null;

            _sut.RunWorkerAsync();

            bool completed = false;
            _sut.RunWorkerCompleted += (sender, args) =>
            {
                res = _sut.GetFiles("X");
                completed = true;
            };

            while (!completed)
            {
                Thread.CurrentThread.Join(100);
            }
            res.Length.Should().Be.EqualTo(0);
        }

        [Test]
        public void GetFiles_ShouldReturnFilesMatchingHash()
        {
            string[] res = null;

            _sut.RunWorkerAsync();

            bool completed = false;
            _sut.RunWorkerCompleted += (sender, args) =>
            {
                res = _sut.GetFiles("A");
                completed = true;
            };

            while (!completed)
            {
                Thread.CurrentThread.Join(100);
            }
            res.Should().Have.SameValuesAs(_f1, _f3);
        }

        [Test]
        public void GetFiles_ShouldRemoveItemsFromCache()
        {
            string[] res = null;
            _sut.RunWorkerAsync();
            bool completed = false;
            _sut.RunWorkerCompleted += (sender, args) =>
            {
                res = _sut.GetFiles("A");
                completed = true;
            };
            while (!completed) { Thread.CurrentThread.Join(100); }
            res.Length.Should().Be(2);

            completed = false;
            FileManager.SafeDelete(_f1);


            _sut.RunWorkerAsync();

            _sut.RunWorkerCompleted += (sender, args) =>
            {
                res = _sut.GetFiles("A");
                completed = true;
            };
            while (!completed) { Thread.CurrentThread.Join(100); }


            res.Length.Should().Be(1);
            res.Should().Have.SameValuesAs(_f3);
        }

        [Test]
        public void ItemsCount_ShouldReturnCachedItemsCount()
        {
            _sut.RunWorkerAsync();

            bool completed = false;
            _sut.RunWorkerCompleted += (sender, args) =>
            {
                completed = true;
            };

            while (!completed)
            {
                Thread.CurrentThread.Join(100);
            }
            _sut.ItemsCount.Should().Be(4);
        }

        public void Dispose()
        {
            _sut.Dispose();
        }
    }


   
}