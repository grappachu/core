using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grappachu.Core.Collections.Extensions;

namespace Grappachu.Core.Collections
{
    /// <summary>
    ///     Extension methods for <see cref="ICollection" /> objects
    /// </summary>
    public static class CollectionUtils
    {
        /// <summary>
        ///     implements the AddRange functionality for any collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="toAdd"></param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> toAdd)
        {
            foreach (var enumerable in toAdd)
                collection.Add(enumerable);
        }


        /// <summary>
        ///     Sorts a collection over a specific comparable in ascending order
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="compareFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <remarks>Implemented with BubbleSort</remarks>
        public static void SortAsc<T>(this ICollection<T> collection, Func<T, IComparable> compareFunc)
        {
            for (var i = 1; i <= collection.Count - 1; ++i)
            for (var j = 0; j < collection.Count - i; ++j)
            {
                var res =
                    compareFunc.Invoke(collection.ElementAt(j))
                        .CompareTo(compareFunc.Invoke(collection.ElementAt(j + 1)));
                if (res > 0)
                {
                    var toShift = collection.ElementAt(j + 1);
                    if (!collection.ShiftFirst(toShift))
                        throw new NullReferenceException();
                }
            }
        }

        /// <summary>
        ///     Sorts a collection over a specific comparable in descending order
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="compareFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <remarks>Implemented with BubbleSort</remarks>
        public static void SortDesc<T>(this ICollection<T> collection, Func<T, IComparable> compareFunc)
        {
            for (var i = 1; i <= collection.Count - 1; ++i)
            for (var j = 0; j < collection.Count - i; ++j)
            {
                var res =
                    compareFunc.Invoke(collection.ElementAt(j))
                        .CompareTo(compareFunc.Invoke(collection.ElementAt(j + 1)));
                if (res < 0)
                {
                    var toShift = collection.ElementAt(j + 1);
                    collection.ShiftFirst(toShift);
                }
            }
        }
    }
}