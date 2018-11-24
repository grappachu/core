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
        ///     Checks if a collection is null or has no elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        ///     Splits a collection into smaller chunks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">collection to split</param>
        /// <param name="chunkSize">The maximum number of items for each chunk</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> ToChunks<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            var chunks = enumerable
                .Select((s, i) => new {Value = s, Index = i})
                .GroupBy(x => x.Index / chunkSize)
                .Select(grp => grp.Select(x => x.Value));
            return chunks;
        }
    }
}