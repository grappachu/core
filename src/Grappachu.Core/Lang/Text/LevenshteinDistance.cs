using System;

namespace Grappachu.Core.Lang.Text
{
    /// <summary>
    ///     The Levenshtein distance between two words is the minimum number of single-character edits
    /// </summary>
    /// <remarks>
    ///     Levenshtein distance is the minimum number of basic changes between two strings A,B to transform A into B
    ///     It's a basic change:
    ///     - single char deletion
    ///     - single char replacement
    ///     - single char added
    /// </remarks>
    public static class LevenshteinDistance
    {
        /// <summary>
        ///     Computes the distance from two strings. This is case sensitive.
        /// </summary>
        public static int Compute(string s, string t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
                return m;

            if (m == 0)
                return n;

            // Step 2
            for (var i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (var i = 1; i <= n; i++)
            for (var j = 1; j <= m; j++)
            {
                // Step 5
                var cost = t[j - 1] == s[i - 1] ? 0 : 1;

                // Step 6
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
            // Step 7
            return d[n, m];
        }

        /// <summary>
        ///     Computes the distance between two strings regadless of the case
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int ComputeIgnoreCase(string s1, string s2)
        {
            return Compute(s1.ToLowerInvariant(), s2.ToLowerInvariant());
        }
    }
}