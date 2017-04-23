using System;
using System.Windows.Forms;

namespace Grappachu.Core.UI
{
    /// <summary>
    /// Rappresenta un semplice gestore per finestre di dialogo comuni con Windows Forms
    /// </summary>
    public class Dialogs : IDialogHolder
    {

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowInfo(string message, string title = "Info")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Error
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="title"></param>
        public static void ShowError(Exception exception, string title = "Error")
        {
            ShowError(exception.Message, title);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowWarning(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Domanda Si/No
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="asWarning">Indica se visualizzare il messaggio con lo stile di un warning</param>
        /// <returns></returns>
        public static bool? ShowQuestion(string message, string title = "Question", bool asWarning = false)
        {
            var res = MessageBox.Show(message, title, MessageBoxButtons.YesNo,
                asWarning ? MessageBoxIcon.Warning : MessageBoxIcon.Question);
            switch (res)
            {
                case DialogResult.Yes:
                    return true;
                case DialogResult.No:
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void Info(string message, string title = "Info")
        {
            ShowInfo(message, title);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void Error(string message, string title = "Error")
        {
            ShowError(message, title);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void Warning(string message, string title = "Warning")
        {
            ShowWarning(message, title);
        }

        /// <summary>
        /// Visualizza una finestra di dialogo di tipo domanda si/no
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="asWarning"></param>
        /// <returns></returns>
        public bool? Question(string message, string title = "Question", bool asWarning = false)
        {
            return ShowQuestion(message, title, asWarning);
        }
    }
}
