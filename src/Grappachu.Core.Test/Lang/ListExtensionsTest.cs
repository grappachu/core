using System.Collections.Generic;
using Grappachu.Core.Lang;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Lang
{
    [TestFixture]
    internal class CollectionExtensionsTest
    {
        [Test]
        public void IndexOf_should_get_base_0_index_of_an_item()
        {
            IEnumerable<object> list = new List<object> {"a", "b", "c", "d", "e"};

            int res = list.IndexOf("c");

            res.Should().Be.EqualTo(2);
        }

        [Test]
        public void IndexOf_should_return_minus_one_if_not_found()
        {
            IEnumerable<object> list = new List<object> {"a", "b", "c", "d", "e"};

            int res = list.IndexOf("f");

            res.Should().Be.EqualTo(-1);
        }

        [Test]
        public void Insert_should_add_a_item_at_a_specified_position()
        {
            ICollection<object> list = new List<object> {"a", "b", "c", "d", "e"};

            list.Insert(2, "x");

            list.Should().Have.SameSequenceAs(new[] {"a", "b", "x", "c", "d", "e"});
        }

        [Test]
        public void MoveDown_should_return_false_if_item_is_at_end()
        {
            ICollection<object> list = new List<object> {"a", "b", "c", "d", "e"};

            bool res = list.MoveDown("e");

            list.Should().Have.SameSequenceAs(new[] {"a", "b", "c", "d", "e"});
            res.Should().Be.False();
        }

        [Test]
        public void MoveDown_should_swap_items()
        {
            ICollection<object> list = new List<object> {"a", "b", "c", "d", "e"};

            bool res = list.MoveDown("c");

            list.Should().Have.SameSequenceAs(new[] {"a", "b", "d", "c", "e"});
            res.Should().Be.True();
        }

        [Test]
        public void MoveUp_should_return_false_if_item_is_at_beginning()
        {
            ICollection<object> list = new List<object> {"a", "b", "c", "d", "e"};

            bool res = list.MoveUp("a");

            list.Should().Have.SameSequenceAs(new[] {"a", "b", "c", "d", "e"});
            res.Should().Be.False();
        }

        [Test]
        public void MoveUp_should_swap_items()
        {
            IList<object> list = new List<object> {"a", "b", "c", "d", "e"};

            bool res = list.MoveUp("c");

            list.Should().Have.SameSequenceAs(new[] {"a", "c", "b", "d", "e"});
            res.Should().Be.True();
        }
    }
}