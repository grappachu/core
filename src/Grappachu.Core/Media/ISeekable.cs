using System;

namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Defines a component capable of locate a point in a time interval
    /// </summary>
    public interface ISeekable : ITimeable
    {
        /// <summary>
        ///     Updates the current position for the <see cref="ISeekable" /> object
        /// </summary>
        /// <param name="time"></param>
        void Seek(TimeSpan time);
    }
}