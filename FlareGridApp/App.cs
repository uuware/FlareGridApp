using System;
using FlareGridApp.Types;

namespace FlareGridApp
{
    /// <summary>
    ///  An app wrapper
    /// </summary>
    public class App
    {
        private Grid grid = new Grid(15, 10);

        public App()
        {
        }

        /// <summary>
        ///  main loop to get commands from console
        /// </summary>
        public void run()
        {
            while (true)
            {
                PrintHelp();
                string? line = Console.ReadLine()?.ToLower();
                switch (line)
                {
                    case "e":
                        Environment.Exit(0);
                        break;
                    case "n":
                        // create a grid
                        var size = InputPoint("Grid size");
                        if (size != null && Grid.IsValidGrid(size.X, size.Y))
                        {
                            grid = new Grid(size.X, size.Y);
                        }
                        else
                        {
                            Log.debug("Grid size is not valid (width and height no less than 5 and no greater than 25)");
                        }
                        break;
                    case "a":
                        // add a rectangle
                        AddRectangle();
                        break;
                    case "f":
                        FindRectangle("Find");
                        break;
                    case "r":
                        // remove a rectangle from a point
                        RemoveRectangle();
                        break;
                    case "d":
                        // display a point
                        Log.debug(grid.DisplayMap());
                        break;
                    default:
                        Log.debug($"Invalid command: [{line}]");
                        break;
                }
            }

        }

        public void AddRectangle()
        {
            var startPoint = InputPoint("Start point");
            if (startPoint != null)
            {
                var endPoint = InputPoint("End point");
                if (endPoint != null)
                {
                    var rectangle = new Rectangle(startPoint, endPoint);
                    if (rectangle.IsValidRectangle(grid.Cols, grid.Rows))
                    {
                        grid.AddRectangle(rectangle);
                    }
                    else
                    {
                        Log.debug("Rectangle is valid, please check the inputs.");
                    }
                }
            }
        }

        public void RemoveRectangle()
        {
            var rectangle = FindRectangle("Find and Remove");
            grid.RemoveRectangle(rectangle);
        }

        public Rectangle? FindRectangle(string msg)
        {
            // find a rectangle from a point
            var point = InputPoint(msg);
            var rectangle = grid.FindRectangle(point);
            if (rectangle != null)
            {
                Console.WriteLine($"Found [{rectangle.StartPoint.X}, {rectangle.StartPoint.Y}] [{rectangle.EndPoint.X}, {rectangle.EndPoint.Y}]");
            }
            else
            {
                Console.WriteLine($"No found");
            }
            return rectangle;
        }

        public Point? InputPoint(string msg)
        {
            Log.debug($"[{msg}]Please input a point with the format: x, y, and Return to finish.");
            string? line = Console.ReadLine();
            var point = Point.Parse(line);
            if (point == null)
            {
                Log.debug($"Invalid point: [{line}]");
            }
            return point;
        }

        private void PrintHelp()
        {
            Log.debug("[Commands] e: Exit, n: New game, a: Add rectangles, f: Find a rectangle, r: Remove a rectangle, d: Display");
        }
    }

}

