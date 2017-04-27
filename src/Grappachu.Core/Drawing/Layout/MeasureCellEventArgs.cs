using System;
using System.Drawing;

namespace Grappachu.Core.Drawing.Layout
{
    /// <summary>
    ///     Encapsulates data for measuring the text size of a single table cell
    /// </summary>
    public class MeasureCellEventArgs : EventArgs
    {
        /// <summary>
        ///     Creates a new instance of <see cref="MeasureCellEventArgs" />
        /// </summary>
        /// <param name="row">index 0-based for the row</param>
        /// <param name="column">index 0-based for the column</param>
        /// <param name="font">font for the text</param>
        /// <param name="cellPadding">padding</param>
        public MeasureCellEventArgs(int row, int column, Font font, int cellPadding)
        {
            Row = row;
            Column = column;
            CellFont = font;
            CellPadding = cellPadding;

            MaxWordWidth = -1;
            MaxTextWidth = -1;
        }

        /// <summary>
        ///     Gets the row index 0-based for the measuring cell
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        ///     Gets the column index 0-based for the measuring cell
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        ///     Gets or sets the font to use for measuring
        /// </summary>
        public Font CellFont { get; set; }

        /// <summary>
        ///     Gets or sets the padding to use for measuring
        /// </summary>
        public int CellPadding { get; set; }

        /// <summary>
        ///     Gets or sets a custom size for the longest word in the measuring text.
        ///     This measure is used to determine the minimum width of the column
        /// </summary>
        public int MaxWordWidth { get; set; }

        /// <summary>
        ///     Gets or sets a custom size for the width of the the measuring text.
        ///     This measure is used to determine the maximum width of the column
        /// </summary>
        public int MaxTextWidth { get; set; }
    }
}