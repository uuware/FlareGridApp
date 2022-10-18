using System;
using System.Drawing;
using System.Net;

namespace FlareGridApp.Types
{
    /// <summary>
    /// A Rectangle of a grid
    /// </summary>
    public class Rectangle
    {
        private Point startPoint, endPoint;
        public Rectangle(Point startPoint, Point endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public Point StartPoint
        {
            get { return startPoint; }
        }
        public Point EndPoint
        {
            get { return endPoint; }
        }

        /// <summary>
        ///  Whether the rectangle contains a point
        /// </summary>
        public bool Contains(Point point)
        {
            if (point.X >= startPoint.X && point.X <= endPoint.X && point.Y >= startPoint.Y && point.Y <= endPoint.Y)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Whether a rectangle is valid in a given grid
        /// </summary>
        public bool IsValidRectangle(int gridCols, int gridRows)
        {
            return IsValidRectangle(gridCols, gridRows, this);
        }

        /// <summary>
        ///  Whether a rectangle is valid in a given grid
        /// </summary>
        static public bool IsValidRectangle(int gridCols, int gridRows, Rectangle rectangle)
        {
            // StartPoint should be ahead of EndPoint
            if (rectangle.EndPoint.X - rectangle.StartPoint.X < 1 || rectangle.EndPoint.Y - rectangle.StartPoint.Y < 1)
            {
                return false;
            }

            // EndPoint should be inside of the grid
            if (!rectangle.EndPoint.IsValidPoint(gridCols, gridRows))
            {
                return false;
            }
            return true;
        }
    }
}

