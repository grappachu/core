using System.Collections;
using System.Collections.Generic;

namespace Grappachu.Core.Collections
{
    /// <summary>
    ///     Extension methods for <see cref="ICollection" /> objects
    /// </summary>
    public static class CollectionUtils
    {
        /// <summary>
        /// implements the AddRange functionality for any collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="toAdd"></param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> toAdd)
        {
            foreach (var enumerable in toAdd)
            {
                collection.Add(enumerable);
            }
        }


    }
}