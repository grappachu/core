using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
}