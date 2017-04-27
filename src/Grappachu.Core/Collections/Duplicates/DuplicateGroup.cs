using System.Collections.Generic;

namespace Grappachu.Core.Collections.Duplicates
{
    /// <summary>
    ///     Represents a generic group for duplicated objects
    /// </summary>
    /// <typeparam name="TK">Key for the duplicate group</typeparam>
    /// <typeparam name="T">Type of duplicated items</typeparam>
    public class DuplicateGroup<TK, T>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DuplicateGroup{TK,T}" />
        /// </summary>
        public DuplicateGroup()
        {
            Items = new List<T>();
        }

        /// <summary>
        ///     Creates a new instance of  <see cref="DuplicateGroup{TK,T}" />
        /// </summary>
        /// <param name="key">Key for grouping duplicates</param>
        public DuplicateGroup(TK key)
            : this()
        {
            Key = key;
        }

        /// <summary>
        ///     Gets or sets the key for the items in the duplicate group
        /// </summary>
        public TK Key { get; set; }

        /// <summary>
        ///     Gets or sets the list of all duplicate objects
        /// </summary>
        public ICollection<T> Items { get; private set; }
    }
}