using System;
using System.Linq;

namespace Grappachu.Core.Lang
{
    /// <summary>
    ///     Defines a set of static and extension methods to work with Enum types
    /// </summary>
    public static class EnumHelper<TEnumType> where TEnumType : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        ///     parses a string into the generic enum type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnumType Parse(string value)
        {
            if (typeof(TEnumType).IsEnum)
            {
                return (TEnumType) Enum.Parse(typeof(TEnumType), value);
            }

            throw new ArgumentException($"{typeof(TEnumType).Name} is not an Enum.");
        }


        /// <summary>
        ///     enum values as a strong typed Enumeration
        /// </summary>
        /// <returns></returns>
        public static TEnumType[] GetValues()
        {
            if (typeof(TEnumType).IsEnum)
                return Enum.GetValues(typeof(TEnumType)).Cast<TEnumType>().ToArray();
            throw new ArgumentException(string.Format("{0} is not an Enum.", typeof(TEnumType).Name));
        }
    }
}