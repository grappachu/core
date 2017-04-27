namespace Grappachu.Core.Preview.UI
{
    /// <summary>
    ///     Defines a basic interface for handling user interaction trough dialogs
    /// </summary>
    public interface IDialogHolder
    {
        /// <summary>
        ///     Displays a Info dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        void Info(string message, string title = "Info");

        /// <summary>
        ///     Displays a Error dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        void Error(string message, string title = "Error");

        /// <summary>
        ///     Displays a Warning dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        void Warning(string message, string title = "Warning");

        /// <summary>
        ///     Displays a simple question dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="asWarning"></param>
        /// <returns></returns>
        bool? Question(string message, string title = "Question", bool asWarning = false);
    }
}