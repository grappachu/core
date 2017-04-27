using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Grappachu.Core.Drawing.Layout
{
    internal static class PixelRuler
    {
        private static readonly string[] WrappingChars = {" ", System.Environment.NewLine};

        internal static int MeasureString(string text, Font font, int padding)
        {
            if (text == null)
                return 0;
            var res = TextRenderer.MeasureText(text.Trim(), font);
            return res.Width + padding;
        }

        internal static int MeasureWord(string text, Font font, int padding)
        {
            if (text == null)
                return 0;
            var parts = text.Split(' ');
            return parts.Max(x => MeasureString(x, font, padding));
        }

        internal static bool CanWrap(string text)
        {
            foreach (var wrappingChar in WrappingChars)
                if (text.Contains(wrappingChar))
                    return true;
            return false;
        }
    }
}