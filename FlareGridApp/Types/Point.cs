using System;
namespace FlareGridApp.Types
{
    /// <summary>
    /// A Point of a grid
    /// </summary>
    public class Point
    {
        private int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

        /// <summary>
        ///  Whether a point is valid in a given grid
        /// </summary>
        public bool IsValidPoint(int gridCols, int gridRows)
        {
            return IsValidPoint(gridCols, gridRows, this);
        }

        /// <summary>
        ///  Whether a point is valid in a given grid
        /// </summary>
        static public bool IsValidPoint(int gridCols, int gridRows, Point point)
        {
            if (point.X < gridCols && point.Y < gridRows)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Parse a string and return a Point if it's valid with the format: x, y
        /// </summary>
        static public Point? Parse(string? line)
        {
            string[]? lines = line?.Split(',');
            if (lines?.Length == 2)
            {
                try
                {
                    var x = int.Parse(lines[0]);
                    var y = int.Parse(lines[1]);
                    if (x >= 0 && y >= 0)
                    {
                        return new Point(x, y);
                    }
                }
                catch (Exception)
                {
                }
            }

            return null;
        }
    }
}

