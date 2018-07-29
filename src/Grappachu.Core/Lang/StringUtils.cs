using System;

namespace Grappachu.Core.Lang
{
    /// <summary>
    /// Defines a set of static and extension methods to work with strings
    /// </summary>
    public static class StringUtils
    {

        /// <summary>
        /// Gets the first substring included between two string chunks
        /// </summary>
        /// <example>
        ///  var street = "Address: Baker Street, 54 - London".Extract(":", ",").Trim();
        ///  // street will be "Baker Street"
        /// </example>
        /// <param name="originalString"></param>
        /// <param name="textBefore"></param>
        /// <param name="textAfter"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string Extract(this string originalString, string textBefore, string textAfter,
            StringComparison comparison = StringComparison.Ordinal)
        {
            return Extract(originalString, textBefore, textAfter, 0, comparison);
        }

        /// <summary>
        /// Gets the first substring included between two string chunks
        /// </summary>
        /// <example>
        ///  var street = "Address: Baker Street, 54 - London".Extract(":", ",").Trim();
        ///  // street will be "Baker Street"
        /// </example>
        /// <param name="originalString"></param>
        /// <param name="textBefore"></param>
        /// <param name="textAfter"></param>
        /// <param name="startIndex">index to start search in the original string</param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string Extract(this string originalString, string textBefore, string textAfter,
            int startIndex, StringComparison comparison = StringComparison.Ordinal)
        {
            var idxStr = originalString.IndexOf(textBefore, startIndex, comparison);
            var idxEnd = originalString.IndexOf(textAfter, idxStr + 1, comparison);
            if (idxStr >= 0 && idxEnd > 0)
            {
                if (idxStr > idxEnd)
                    Swap(ref idxStr, ref idxEnd);
                return originalString.Substring(idxStr + textBefore.Length, idxEnd - idxStr - textBefore.Length);
            }
            return originalString;
        }



        private static void Swap<T>(ref T idxStr, ref T idxEnd)
        {
            var temp = idxEnd;
            idxEnd = idxStr;
            idxStr = temp;
        }


        /// <summary>
        ///     Checks if a string matches a text using ignoring case conventions
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string source, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

    }
}
