using System;

namespace Grappachu.Core.Lang.Domain
{
    /// <summary>
    ///     Represents a generic class for classes that contain event data for errors.
    /// </summary>
    public class ErrorArgs : EventArgs
    {
        /// <summary>
        ///     Creates a new instance of <see cref="ErrorArgs" />
        /// </summary>
        public ErrorArgs()
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="ErrorArgs" />
        /// </summary>
        /// <param name="ex"></param>
        public ErrorArgs(Exception ex)
        {
            Messsage = ex.Message;
            Detail = ex.ToString();
        }

        /// <summary>
        ///     Gets or sets the error message
        /// </summary>
        public string Messsage { get; set; }

        /// <summary>
        ///     Gets or sets a detailed description for the error
        /// </summary>
        public string Detail { get; set; }
    }
}