using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Grappachu.Core.Collections;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Collections
{
    [TestFixture]
    public class CollectionUtilsTest
    {
        [Test]
        public void AddRange_should_add_a_list_of_objects()
        {
            ICollection<object> coll = new Collection<object>();
            object[] fullArray = { "ciao", 2, DateTime.Now };

            coll.AddRange(fullArray);

            coll.Should().Have.SameValuesAs(fullArray);
        }


        [Test]
        public void SortAsc_should_order_a_collection_ascending()
        {
            var data = new[] { new Point(1, 2), new Point(2, 3), new Point(5, 1), new Point(8, 4), new Point(1, 5) };
            ICollection<Point> coll = new List<Point>(data);

            coll.SortAsc(x => x.Y);

            coll.Select(c => c.Y).Should().Have.SameSequenceAs(1, 2, 3, 4, 5);
        }


        [Test]
        public void SortDesc_should_order_a_collection_descending()
        {
            var data = new[] { new Point(1, 2), new Point(2, 3), new Point(5, 1), new Point(8, 4), new Point(1, 5) };
            ICollection<Point> coll = new List<Point>(data);

            coll.SortDesc(x => x.Y);

            coll.Select(c => c.Y).Should().Have.SameSequenceAs(5, 4, 3, 2, 1);
        }

    }
}