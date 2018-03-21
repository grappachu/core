using System;

namespace Grappachu.Core.Lang
{
    /// <summary>
    ///     Defines some useful DateTime constraints
    /// </summary>
    public static class DateTimes
    {
        /// <summary>
        ///     Defines a date matching the maximum acceptable value for a SqlServer datetime (January 1, 1753)
        /// </summary>
        public static readonly DateTime SqlMinValue = new DateTime(1753, 1, 1, 0, 0, 0, 0);

        /// <summary>
        ///     Defines a date matching the maximum acceptable value for a SqlServer datetime (December 31, 9999)
        /// </summary>
        public static readonly DateTime SqlMaxValue = new DateTime(9999, 12, 31, 23, 59, 59, 998);

        /// <summary>
        ///     Defines a date matching 1st Jan 1970
        /// </summary>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    }
}