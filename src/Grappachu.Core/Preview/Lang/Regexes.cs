namespace Grappachu.Core.Preview.Lang
{
    /// <summary>
    ///     Defines a collection of regex strings for common validations
    /// </summary>
    public static class Regexes
    {
        /// <summary>
        /// Defines a string for validating email addresses
        /// </summary>
        public const string Email = "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$";


        /// <summary>
        /// Defines a string for validating tax codes for Italy
        /// </summary>
        public const string TaxCodeItaly = "^[A-Za-z]{6}[0-9]{2}[A-Za-z]{1}[0-9]{2}[A-Za-z]{1}[0-9]{3}[A-Za-z]{1}$";
    }
}