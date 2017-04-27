using System;
using System.Data;
using System.Drawing;

namespace Grappachu.Core.Drawing.Layout
{
    /// <summary>
    ///     Defines a component for computing sizes of <see cref="DataTable" /> columns.
    /// </summary>
    public sealed class TableLayoutEngine
    {
        /// <summary>
        ///     Creates a new instance of <see cref="TableLayoutEngine" />
        /// </summary>
        /// <param name="defaultFont"></param>
        public TableLayoutEngine(Font defaultFont)
        {
            DefaultFont = defaultFont;
            DefaultCellPadding = 4;
            TableBorderSize = 1;
        }

        /// <summary>
        ///     Gets or sets the cell padding in in pixels
        /// </summary>
        public int DefaultCellPadding { get; set; }

        /// <summary>
        ///     Gets or sets the default font used for computing column sizes
        /// </summary>
        public Font DefaultFont { get; set; }

        /// <summary>
        ///     Gets or sets a width of cell borders in pixels
        /// </summary>
        public int TableBorderSize { get; set; }

        /// <summary>
        ///     Raised when measuring the size of a single table cell
        /// </summary>
        public event EventHandler<MeasureCellEventArgs> MeasuringCell;

        private void OnMeasuringCell(MeasureCellEventArgs e)
        {
            MeasuringCell?.Invoke(this, e);
        }

        /// <summary>
        ///     Computes the optimal size for all columns in a table
        ///     and return an array of widths in pixels
        /// </summary>
        /// <param name="table">table to compute</param>
        /// <param name="preferredWidth">desired width (pixels) to fit in</param>
        /// <returns></returns>
        public int[] GetColumnSizes(DataTable table, int preferredWidth)
        {
            var columns = table.Columns.Count;

            var width = preferredWidth;

            var maxText = new int[columns]; // max. text width over all rows
            var maxWord = new int[columns]; // max. width of longest word
            var isFlex = new bool[columns]; // is column flexible?
            var canWrap = new bool[columns]; // can column be wrapped?
            var colw = new int[columns]; // final width of columns

            for (var r = 0; r < table.Rows.Count; r++)
            {
                var row = table.Rows[r];

                for (var i = 0; i < columns; i++)
                {
                    var args = new MeasureCellEventArgs(r, i, DefaultFont, DefaultCellPadding);
                    OnMeasuringCell(args);

                    var cell = string.Format("{0}", row[i]);
                    var pad = 2 * args.CellPadding;

                    maxText[i] = args.MaxTextWidth >= 0
                        ? args.MaxTextWidth
                        : Math.Max(maxText[i], PixelRuler.MeasureString(cell, args.CellFont, pad));

                    if (args.MaxWordWidth >= 0)
                    {
                        maxWord[i] = args.MaxWordWidth;
                    }
                    else
                    {
                        if (PixelRuler.CanWrap(cell))
                        {
                            canWrap[i] = true;
                            maxWord[i] = Math.Max(maxWord[i], PixelRuler.MeasureWord(cell, args.CellFont, pad));
                        }
                        else
                        {
                            maxWord[i] = maxText[i];
                        }
                    }
                }

                var left = width - (columns - 1) * TableBorderSize;
                var avg = left / columns;
                var nflex = 0;


                // Determine whether columns should be flexible and assign width of non-flexible cells
                for (var i = 0; i < columns; i++)
                {
                    isFlex[i] = maxText[i] > 2 * avg;
                    if (isFlex[i])
                    {
                        nflex++;
                    }
                    else
                    {
                        colw[i] = maxText[i];
                        left -= colw[i];
                    }
                }

                // if there is not enough space, make columns that could be word-wrapped flexible, too
                if (left < nflex * avg)
                    for (var i = 0; i < columns; i++)
                        if (!isFlex[i] && canWrap[i])
                        {
                            left += width; //???
                            colw[i] = 0;
                            isFlex[i] = true;
                            nflex += 1;
                        }

                // Calculate weights for flexible columns. The max width  is capped at the page width 
                // to treat columns that have to   be wrapped more or less equal
                var tot = 0;
                for (var i = 0; i < columns; i++)
                    if (isFlex[i])
                    {
                        maxText[i] = Math.Min(maxText[i], width);
                        tot += maxText[i];
                    }


                // Assign the actual width for flexible columns. 
                // Make sure that it is at least as long as the longest word length
                for (var i = 0; i < columns; i++)
                    if (isFlex[i])
                    {
                        if (tot > 0)
                            colw[i] = left * maxText[i] / tot;
                        colw[i] = Math.Max(colw[i], maxWord[i]);
                        left -= colw[i];
                    }
            }

            return colw;
        }


    }
}