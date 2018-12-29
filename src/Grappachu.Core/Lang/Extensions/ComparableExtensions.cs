using System;

namespace Grappachu.Core.Lang.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="IComparable{T}" /> objects
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        ///     Gets true when the item is included in a interval (including bounds)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subject"></param>
        /// <param name="inclusiveLowerBound"></param>
        /// <param name="inclusiveHigherBound"></param>
        /// <returns></returns>
        public static bool Between<T>(this T subject, T inclusiveLowerBound, T inclusiveHigherBound)
            where T : IComparable
        {
            return subject.CompareTo(inclusiveLowerBound) >= 0 && subject.CompareTo(inclusiveHigherBound) <= 0;
        }


        /// <summary>
        ///     Gets true when the item is strictly included (excluding bounds) in a interval.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subject"></param>
        /// <param name="exclusiveLowerBound"></param>
        /// <param name="exclusiveHigherBound"></param>
        /// <returns></returns>
        public static bool BetweenStrict<T>(this T subject, T exclusiveLowerBound, T exclusiveHigherBound)
            where T : IComparable
        {
            return subject.CompareTo(exclusiveLowerBound) > 0 && subject.CompareTo(exclusiveHigherBound) < 0;
        }

        /// <summary>
        ///     Checks the <paramref name="preferred" /> value and returns the <paramref name="alternative" /> when the first one
        ///     is null.
        /// </summary>
        /// <remarks>Note: When the value is a primitive type or enum the default(T) will be considered a non null value</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="preferred"></param>
        /// <param name="alternative"></param>
        /// <returns></returns>
        public static T Or<T>(this T preferred, T alternative) where T : IComparable
        {
            if (preferred == null)
                return alternative;
            return preferred.CompareTo(default(T)) != 0 ? preferred : alternative;
        }

        /// <summary>
        ///     Checks the <paramref name="maybeNullValue" /> value and returns itself when not null. Otherwise throws a
        ///     <see cref="NullReferenceException" />  whith the provided error message.
        /// </summary>
        /// <param name="maybeNullValue">The parameter to check</param>
        /// <param name="errorMessage">The error message supplied</param>
        /// <exception cref="NullReferenceException">When the supplied value is null</exception>
        /// <returns>A non null object</returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     setupRequest.Mode.OrDie("You didn't specify a installation mode");
        ///     setupRequest.TargetFolder.OrDie("You didn't specify your target folder");
        ///
        ///     // request is valid
        ///     _processor.Process(request);
        /// }
        /// catch(NullReferenceExteption ex)
        /// {
        ///     NotifyError(ex.Message);
        /// }
        /// </code>  
        /// </example>
        public static T OrDie<T>(this T maybeNullValue, string errorMessage)
        {
            if (maybeNullValue == null)
            {
                throw new NullReferenceException(errorMessage);
            }

            return maybeNullValue;
        } 
    }
}