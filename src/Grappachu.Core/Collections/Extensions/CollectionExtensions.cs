using System.Collections;
using System.Collections.Generic;

namespace Grappachu.Core.Collections.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="ICollection" /> objects
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Finds the <paramref name="obj" /> in the collection and returns the first occurrence of it.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        /// <returns>The index of the item zero based or -1 when object is not found</returns>
        public static int IndexOf<T>(this IEnumerable collection, T obj)
        {
            var e = collection.GetEnumerator();
            var i = 0;
            while (e.MoveNext())
            {
                if (ReferenceEquals(e.Current, obj))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        ///     Allows to add an element into a <see cref="ICollection{T}" />
        ///     at the <paramref name="index" /> specified
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <param name="obj"></param>
        public static void Insert<T>(this ICollection<T> collection, int index, T obj)
        {
            var reuse = new List<T>();
            using (var e = collection.GetEnumerator())
            {
                for (var j = 0; j < collection.Count; j++)
                {
                    e.MoveNext();
                    if (j >= index)
                        reuse.Add(e.Current);
                }
            }
            collection.Add(obj);
            foreach (var x in reuse)
                if (collection.Remove(x))
                    collection.Add(x);
        }

        /// <summary>
        ///     Moves an element of the collection one step to the begin
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ShiftFirst<T>(this ICollection<T> collection, T obj)
        {
            var curIdx = collection.IndexOf(obj);
            if (curIdx > 0)
            {
                var newIdx = curIdx - 1;
                if (collection.Remove(obj))
                {
                    collection.Insert(newIdx, obj);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Moves an element of the collection one step to the end
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ShiftLast<T>(this ICollection<T> collection, T obj)
        {
            var curIdx = collection.IndexOf(obj);
            if (curIdx < collection.Count - 1)
            {
                var newIdx = curIdx + 1;
                if (collection.Remove(obj))
                {
                    collection.Insert(newIdx, obj);
                    return true;
                }
            }
            return false;
        }
    }
}