using System;
using System.Drawing;

namespace Grappachu.Core.Drawing.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class RectangeFExtension
    {
        /// <summary>
        ///     Gets a point in the center of rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static PointF GetCenterF(this RectangleF rect)
        {
            float centerW = (rect.X * 2 + rect.Width) / 2;
            float centerH = (rect.Y * 2 + rect.Height) / 2;
            var point = new PointF(centerW, centerH);
            return point;
        }

        /// <summary>
        ///     Gets a point in the center of rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Point GetCenter(this RectangleF rect)
        {
            var centerW = (int)Math.Round((rect.X * 2 + rect.Width) / 2);
            var centerH = (int)Math.Round((rect.Y * 2 + rect.Height) / 2);
            var point = new Point(centerW, centerH);
            return point;
        }

        /// <summary>
        ///     Checks if a rectangle covers a point on X-Axis
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="xPoint"></param>
        /// <returns></returns>
        public static bool IntersectX(this RectangleF rect, float xPoint)
        {
            return xPoint >= rect.Left && xPoint <= (rect.Left + rect.Width);
        }

        /// <summary>
        ///     Checks if a rectangle covers a point on Y-Axis
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="yPoint"></param>
        /// <returns></returns>
        public static bool IntersectY(this RectangleF rect, float yPoint)
        {
            return yPoint >= rect.Top && yPoint <= (rect.Top + rect.Height);
        }
    }
}