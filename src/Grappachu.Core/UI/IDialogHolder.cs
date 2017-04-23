namespace Grappachu.Core.UI
{
    /// <summary>
    /// Rappresenta un semplice gestore per finestre di dialogo comuni.
    /// </summary>
    public interface IDialogHolder
    {
        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        void Info(string message, string title = "Info");

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        void Error(string message, string title = "Error");

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        void Warning(string message, string title = "Warning");

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo domanda si/no
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="asWarning"></param>
        /// <returns></returns>
        bool? Question(string message, string title = "Question", bool asWarning = false);
    }
}