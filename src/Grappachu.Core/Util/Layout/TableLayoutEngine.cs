using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Grappachu.Core.Util.Layout
{
    /// <summary>
    /// Componente per il calcolo di un layout di tabella.
    /// </summary>
    public sealed class TableLayoutEngine
    {
        /// <summary>
        /// Richiamato al momento della misurazione di una cella
        /// </summary>
        public event EventHandler<MeasureCellEventArgs> MeasuringCell;

        /// <summary>
        /// Invocatore per l'evento <see cref="MeasuringCell"/>
        /// </summary>
        /// <param name="e"></param>
        private void OnMeasuringCell(MeasureCellEventArgs e)
        {
            EventHandler<MeasureCellEventArgs> handler = MeasuringCell;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Attiva una nuova istanza di <see cref="TableLayoutEngine"/>
        /// </summary>
        /// <param name="dataRows"></param>
        public TableLayoutEngine(Font dataRows)
        {
            DefaultFont = dataRows;
            DefaultCellPadding = 4;
            TableBorderSize = 1;
        }

        /// <summary>
        ///     Ottiene o imposta il margine interno della cella in pixel
        /// </summary>
        public int DefaultCellPadding { get; set; }

        /// <summary>
        ///     Ottiene o imposta il font predefinito della tabella
        /// </summary>
        public Font DefaultFont { get; set; }

        /// <summary>
        ///     Ottiene o imposta la larghhezza del bordi interni della tabella in pixel
        /// </summary>
        public int TableBorderSize { get; set; }



        private static int MeasureString(string text, Font font, int padding)
        {
            if (text == null)
            {
                return 0;
            }
            Size res = TextRenderer.MeasureText(text.Trim(), font);
            return res.Width + padding;
        }

        private static int MeasureWord(string text, Font font, int padding)
        {
            if (text == null)
            {
                return 0;
            }
            string[] parts = text.Split(' ');
            return parts.Max(x => MeasureString(x, font, padding));
        }

        /// <summary>
        /// Calcola la dimensione (in pixel) delle colonne della tabella adattandola alla larghezza finale
        /// </summary>
        /// <param name="dt">tabella da valutare</param>
        /// <param name="expectedTableWidthPx">Larghezza finale della tabella attesa</param>
        /// <returns></returns>
        public int[] GetColumnSizes(DataTable dt, int expectedTableWidthPx)
        {
            int columns = dt.Columns.Count;

            int width = expectedTableWidthPx;

            var maxText = new int[columns]; // max. text width over all rows
            var maxWord = new int[columns]; // max. width of longest word
            var isFlex = new bool[columns]; // is column flexible?
            var canWrap = new bool[columns]; // can column be wrapped?
            var colw = new int[columns]; // final width of columns

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow row = dt.Rows[r];

                for (int i = 0; i < columns; i++)
                {
                    var args = new MeasureCellEventArgs(r, i, DefaultFont, DefaultCellPadding);
                    OnMeasuringCell(args);

                    string cell = string.Format("{0}", row[i]);
                    var pad = (2 * args.CellPadding);

                    maxText[i] = (args.MaxTextWidth >= 0)
                        ? args.MaxTextWidth
                        : Math.Max(maxText[i], MeasureString(cell, args.CellFont, pad));

                    if (args.MaxWordWidth >= 0)
                    {
                        maxWord[i] = args.MaxWordWidth;
                    }
                    else
                    {
                        if (CanWrap(cell))
                        {
                            canWrap[i] = true;
                            maxWord[i] = Math.Max(maxWord[i], MeasureWord(cell, args.CellFont, pad));
                        }
                        else
                        {
                            maxWord[i] = maxText[i];
                        }
                    }

                }

                int left = width - (columns - 1) * TableBorderSize;
                int avg = left / columns;
                int nflex = 0;


                //# determine whether columns should be flexible and assign
                //# width of non-flexible cells
                for (int i = 0; i < columns; i++)
                {
                    isFlex[i] = (maxText[i] > 2 * avg);
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

                //# if there is not enough space, make columns that could
                //# be word-wrapped flexible, too
                if (left < nflex * avg)
                {
                    for (int i = 0; i < columns; i++)
                    {
                        if (!isFlex[i] && canWrap[i])
                        {
                            left += width; //???
                            colw[i] = 0;
                            isFlex[i] = true;
                            nflex += 1;
                        }
                    }
                }

                //# Calculate weights for flexible columns. The max width
                //# is capped at the page width to treat columns that have to 
                //# be wrapped more or less equal
                int tot = 0;
                for (int i = 0; i < columns; i++)
                {
                    if (isFlex[i])
                    {
                        maxText[i] = Math.Min(maxText[i], width);
                        tot += maxText[i];
                    }
                }


                //# Now assign the actual width for flexible columns. Make
                //# sure that it is at least as long as the longest word length
                for (int i = 0; i < columns; i++)
                {
                    if (isFlex[i])
                    {
                        if (tot > 0)
                        {
                            colw[i] = left * maxText[i] / tot;
                        }
                        colw[i] = Math.Max(colw[i], maxWord[i]);
                        left -= colw[i];
                    }
                }
            }

            return colw;
        }

        private static bool CanWrap(string cell)
        {
            return cell.Contains(" ") || cell.Contains(System.Environment.NewLine);
        }
    }

    /// <summary>
    /// Racchiude i dati per la misurazione di una cella di una tabella
    /// </summary>
    public class MeasureCellEventArgs : EventArgs
    {
        /// <summary>
        /// Crea una nuova istanza di <see cref="MeasureCellEventArgs"/>
        /// </summary>
        /// <param name="row">indice (base 0 ) della riga</param>
        /// <param name="column">indice (base 0) della colonna</param>
        /// <param name="font">font della cella</param>
        /// <param name="cellPadding">margine interno della cella</param>
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
        /// Ottiene la riga (base 0) della cella che si sta misurando
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Ottiene la colonna (base 0) della cella che si sta misurando
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Ottiene o imposta il font da considerare per la cella che si sta misurando
        /// </summary>
        public Font CellFont { get; set; }

        /// <summary>
        /// Ottiene o imposta il margine interno da considerare per la cella che si sta misurando
        /// </summary>
        public int CellPadding { get; set; }

        /// <summary>
        /// Permette di impostare una dimensione personalizzata che rappresenta la larghezza minima della cella
        /// </summary>
        public int MaxWordWidth { get; set; }

        /// <summary>
        /// Permette di impostare una dimensione personalizzata che rappresenta la larghezza massima della cella
        /// </summary>
        public int MaxTextWidth { get; set; }

    }
}
