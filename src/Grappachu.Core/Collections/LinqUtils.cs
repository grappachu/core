using System;
using System.Collections.Generic;
using System.Linq;

namespace Grappachu.Core.Collections
{
    /// <summary>
    ///     Adds some method extensions for LINQ
    /// </summary>
    public static class LinqUtils
    {
        /// <summary>
        ///     Takes the last (n) items from a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int numberOfItems)
        {
            return source.Skip(Math.Max(0, source.Count() - numberOfItems));
        }
    }
}