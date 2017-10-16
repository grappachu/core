using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Grappachu.Core.Collections
{
    /// <summary>
    ///     Extension methods for <see cref="IEnumerable" /> objects
    /// </summary>
    public static class EnumerableUtils
    {
        /// <summary>
        /// Checks if a collection is null or has no elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}