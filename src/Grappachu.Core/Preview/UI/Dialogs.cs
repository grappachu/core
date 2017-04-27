using System;
using System.Windows.Forms;

namespace Grappachu.Core.Preview.UI
{
    /// <summary>
    ///     Defines a simple dialog implementation based on Windows Forms
    /// </summary>
    public class Dialogs : IDialogHolder
    {
        /// <summary>
        ///     Displays a Info dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void Info(string message, string title = "Info")
        {
            ShowInfo(message, title);
        }

        /// <summary>
        ///     Displays a Error dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void Error(string message, string title = "Error")
        {
            ShowError(message, title);
        }

        /// <summary>
        ///     Displays a Warning dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void Warning(string message, string title = "Warning")
        {
            ShowWarning(message, title);
        }

        /// <summary>
        ///     Displays a simple question dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="asWarning"></param>
        /// <returns></returns>
        public bool? Question(string message, string title = "Question", bool asWarning = false)
        {
            return ShowQuestion(message, title, asWarning);
        }

        /// <summary>
        ///     Displays a Info dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowInfo(string message, string title = "Info")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        ///     Displays a Error dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        ///     Displays a Error dialog
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="title"></param>
        public static void ShowError(Exception exception, string title = "Error")
        {
            ShowError(exception.Message, title);
        }

        /// <summary>
        ///     Displays a Warning dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowWarning(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        ///     Displays a simple question dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="asWarning"></param>
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
    }
}