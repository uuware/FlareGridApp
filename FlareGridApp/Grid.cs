using System;
using System.Collections;
using System.Drawing;
using System.Text;
using FlareGridApp.Types;
using Point = FlareGridApp.Types.Point;
using Rectangle = FlareGridApp.Types.Rectangle;

namespace FlareGridApp
{
    /// <summary>
    ///  A grid to maintain rectangles
    /// </summary>
    public class Grid
    {
        public static int POINT_MIN_LENGTH = 5;
        public static int POINT_MAX_LENGTH = 25;

        // added rectangles list
        private List<Rectangle> rectangles = new List<Rectangle>();
        // grid's bit map for added rectangles
        private bool[][] gridMap;
        private int cols, rows;

        public Grid(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            gridMap = CreateGrid(cols, rows);
            Log.debug($"A grid is created with cols: {cols}, rows: {rows}");
        }

        public int Cols
        {
            get { return cols; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public List<Rectangle> Rectangles
        {
            get { return rectangles; }
        }

        // A grid must have a width and height of no less than 5 and no greater than 25
        static public bool IsValidGrid(int cols, int rows)
        {
            if (cols < 1 || rows < 1)
            {
                return false;
            }

            var length = cols + rows;
            if (length < POINT_MIN_LENGTH || length > POINT_MAX_LENGTH)
            {
                return false;
            }
            return true;
        }

        public bool IsOverlappingRectangle(Rectangle rectangle)
        {
            if (rectangle.StartPoint.X < 0 || rectangle.StartPoint.Y < 0
                || rectangle.EndPoint.X >= cols || rectangle.EndPoint.Y >= rows)
            {
                return true;
            }
            for (int i = rectangle.StartPoint.X; i <= rectangle.EndPoint.X; ++i)
            {
                for (int j = rectangle.StartPoint.Y; j <= rectangle.EndPoint.Y; ++j)
                {
                    // rows, cols
                    if (gridMap[j][i])
                    {
                        // if left axis is overlapping, and next line is not set, then it's ok
                        if (i == rectangle.StartPoint.X && !gridMap[j][i + 1])
                        {
                        }
                        // if top axis is overlapping, and next line is not set, then it's ok
                        else if (j == rectangle.StartPoint.Y && !gridMap[j + 1][i])
                        {
                        }
                        // if bottom axis is overlapping, then it's ok
                        else if (i == rectangle.EndPoint.X)
                        {
                        }
                        // if right axis is overlapping, then it's ok
                        else if (j == rectangle.EndPoint.Y)
                        {
                        }
                        else
                        {
                            Log.debug($"Overlapping at: {i}, {j}");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool AddRectangle(Rectangle rectangle)
        {
            if (!rectangle.IsValidRectangle(cols, rows))
            {
                return false;
            }
            if (IsOverlappingRectangle(rectangle))
            {
                return false;
            }
            rectangles.Add(rectangle);
            SetMap(rectangle);
            Log.debug($"Finial rectangle count after added: {rectangles.Count}");
            return true;
        }

        public Rectangle? FindRectangle(Point? point)
        {
            if (point == null)
            {
                return null;
            }
            for (int i = 0; i < rectangles.Count; ++i)
            {
                if (rectangles[i].Contains(point))
                {
                    return rectangles[i];
                }

            }
            return null;
        }

        public void RemoveRectangle(Rectangle? rectangle)
        {
            if (rectangle != null)
            {
                rectangles.Remove(rectangle);
                Log.debug($"Finial rectangle count after removed: {rectangles.Count}");

                // reset the map
                ResetMap();
            }
        }

        /*
         * Create a rows x cols map.
         * row 0: col 0, col 1, ...., col n
         * row 1: col 0, col 1, ...., col n
         * ...
         * row n: col 0, col 1, ...., col n
         */
        private bool[][] CreateGrid(int cols, int rows)
        {
            bool[][] result = new bool[rows][];
            for (int i = 0; i < rows; ++i)
            {
                result[i] = new bool[cols];
                Array.Clear(result[i], 0, cols);
            }

            return result;
        }

        private void SetMap(Rectangle rectangle)
        {
            for (int i = rectangle.StartPoint.X; i <= rectangle.EndPoint.X; ++i)
            {
                for (int j = rectangle.StartPoint.Y; j <= rectangle.EndPoint.Y; ++j)
                {
                    // rows, cols
                    gridMap[j][i] = true;
                }
            }
        }

        private void ResetMap()
        {
            gridMap = CreateGrid(cols, rows);
            foreach(var rectangle in rectangles)
            {
                SetMap(rectangle);
            }
        }

        public string DisplayMap()
        {
            // for each rows, it has label, cols and enter
            StringBuilder sb = new StringBuilder("   ", (cols + 1 + 1) * (rows + 1) * 3);
            for (int j = 0; j < cols; ++j)
            {
                sb.Append(j.ToString().PadRight(3));
            }
            sb.Append("\n");
            for (int i = 0; i < rows; ++i)
            {
                sb.Append(i.ToString().PadRight(3));
                for (int j = 0; j < cols; ++j)
                {
                    sb.Append(gridMap[i][j] ? " * " : " - ");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}

