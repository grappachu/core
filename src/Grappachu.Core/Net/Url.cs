using System;

namespace Grappachu.Core.Net
{
    /// <summary>
    /// Fornisce metodi di utilità per la manipolazione degli Url
    /// </summary>
    public static class Url
    {
        /// <summary>
        /// Concatena due frammenti di testo che compongono un Url
        /// </summary>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <returns></returns>
        public static string Combine(string part1, string part2)
        {
            if (string.IsNullOrEmpty(part1))
            {
                return new Uri(part2, UriKind.RelativeOrAbsolute).ToString();
            }
            if (string.IsNullOrEmpty(part2))
            {
                return new Uri(part1, UriKind.RelativeOrAbsolute).ToString();
            }
            if (part1.EndsWith("?") || part2.StartsWith("?"))
            {
                part1 = part1.TrimEnd('?');
                part2 = part2.TrimStart('?');
                return new Uri(string.Format("{0}?{1}", part1, part2), UriKind.RelativeOrAbsolute).ToString();
            }
            part1 = part1.TrimEnd('/');
            part2 = part2.TrimStart('/');
            return new Uri(string.Format("{0}/{1}", part1, part2), UriKind.RelativeOrAbsolute).ToString();
        }
    }
}