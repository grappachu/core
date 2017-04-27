using System;

namespace Grappachu.Core.Media
{
    /// <summary>
    ///     Defines a component that interacts in a time interval
    /// </summary>
    public interface ITimeable
    {
        /// <summary>
        ///     Gets the time position for the current interaction
        /// </summary>
        TimeSpan Position { get; }

        /// <summary>
        ///     Gets the total time interval where the <see cref="Position" /> can move
        /// </summary>
        TimeSpan Duration { get; }
    }
}