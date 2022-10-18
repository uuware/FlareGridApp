namespace FlareGridTests;
using FlareGridApp;
using FlareGridApp.Types;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json.Linq;

[TestClass]
public class GridTest
{
    [TestMethod]
    public void Test_IsValidGrid()
    {
        Assert.IsTrue(Grid.IsValidGrid(Grid.POINT_MIN_LENGTH - 1, 1));
        Assert.IsTrue(Grid.IsValidGrid(1, Grid.POINT_MIN_LENGTH - 1));
        Assert.IsTrue(Grid.IsValidGrid(1, Grid.POINT_MAX_LENGTH - 1));
        Assert.IsTrue(Grid.IsValidGrid(Grid.POINT_MAX_LENGTH - 1, 1));

        Assert.IsFalse(Grid.IsValidGrid(Grid.POINT_MIN_LENGTH - 2, 1));
        Assert.IsFalse(Grid.IsValidGrid(1, Grid.POINT_MIN_LENGTH - 2));
        Assert.IsFalse(Grid.IsValidGrid(1, Grid.POINT_MAX_LENGTH));
        Assert.IsFalse(Grid.IsValidGrid(Grid.POINT_MAX_LENGTH, 1));
    }

    [TestMethod]
    public void Test_Add_Remove_Rectangle()
    {
        Grid grid = new Grid(8, 4);
        grid.AddRectangle(new Rectangle(new Point(1, 1), new Point(6, 3)));
        Assert.IsTrue(grid.Rectangles.Count == 1);

        var rect = grid.FindRectangle(new Point(1, 2));
        Assert.IsNotNull(rect);

        grid.RemoveRectangle(rect);
        Assert.IsTrue(grid.Rectangles.Count == 0);

        grid.RemoveRectangle(null);
        Assert.IsTrue(grid.Rectangles.Count == 0);
    }

    [TestMethod]
    public void Test_Find_Rectangle()
    {
        Grid grid = new Grid(8, 4);
        grid.AddRectangle(new Rectangle(new Point(1, 1), new Point(6, 3)));
        Assert.IsTrue(grid.Rectangles.Count == 1);

        Assert.IsNotNull(grid.FindRectangle(new Point(1, 1)));
        Assert.IsNotNull(grid.FindRectangle(new Point(6, 3)));
        Assert.IsNotNull(grid.FindRectangle(new Point(2, 2)));
        Assert.IsNotNull(grid.FindRectangle(new Point(5, 2)));

        Assert.IsNull(grid.FindRectangle(new Point(0, 0)));
        Assert.IsNull(grid.FindRectangle(new Point(0, 1)));
        Assert.IsNull(grid.FindRectangle(new Point(6, 4)));
        Assert.IsNull(grid.FindRectangle(new Point(50, 50)));
    }

    [TestMethod]
    public void Test_IsOverlappingRectangle()
    {
        Grid grid = new Grid(8, 5);
        grid.AddRectangle(new Rectangle(new Point(1, 1), new Point(6, 3)));
        Assert.IsTrue(grid.IsOverlappingRectangle(new Rectangle(new Point(1, 1), new Point(2, 2))));
        Assert.IsTrue(grid.IsOverlappingRectangle(new Rectangle(new Point(5, 2), new Point(6, 3))));

        // overlap rectangle
        Assert.IsTrue(grid.IsOverlappingRectangle(new Rectangle(new Point(0, 0), new Point(2, 2))));
        Assert.IsTrue(grid.IsOverlappingRectangle(new Rectangle(new Point(5, 2), new Point(7, 3))));

        // inside rectangle
        Assert.IsTrue(grid.IsOverlappingRectangle(new Rectangle(new Point(2, 1), new Point(5, 2))));
        Assert.IsTrue(grid.IsOverlappingRectangle(new Rectangle(new Point(2, 2), new Point(3, 3))));

        // overlap the left line
        Assert.IsFalse(grid.IsOverlappingRectangle(new Rectangle(new Point(0, 0), new Point(1, 4))));
        // overlap the top line
        Assert.IsFalse(grid.IsOverlappingRectangle(new Rectangle(new Point(0, 0), new Point(4, 1))));
        // overlap the right line
        Assert.IsFalse(grid.IsOverlappingRectangle(new Rectangle(new Point(6, 0), new Point(7, 3))));
        // overlap the bottom line
        Assert.IsFalse(grid.IsOverlappingRectangle(new Rectangle(new Point(0, 3), new Point(4, 4))));
    }


    [TestMethod]
    public void Test_DisplayMap()
    {
        Grid grid = new Grid(8, 5);
        grid.AddRectangle(new Rectangle(new Point(1, 1), new Point(3, 3)));
        var map1 = @"   0  1  2  3  4  5  6  7  
0   -  -  -  -  -  -  -  - 
1   -  *  *  *  -  -  -  - 
2   -  *  *  *  -  -  -  - 
3   -  *  *  *  -  -  -  - 
4   -  -  -  -  -  -  -  - 
";
        Assert.AreEqual(map1, grid.DisplayMap());

        grid.AddRectangle(new Rectangle(new Point(3, 2), new Point(5, 4)));
        var map2 = @"   0  1  2  3  4  5  6  7  
0   -  -  -  -  -  -  -  - 
1   -  *  *  *  -  -  -  - 
2   -  *  *  *  *  *  -  - 
3   -  *  *  *  *  *  -  - 
4   -  -  -  *  *  *  -  - 
";
        Assert.AreEqual(map2, grid.DisplayMap());

        grid.AddRectangle(new Rectangle(new Point(5, 0), new Point(7, 1)));
        var map3 = @"   0  1  2  3  4  5  6  7  
0   -  -  -  -  -  *  *  * 
1   -  *  *  *  -  *  *  * 
2   -  *  *  *  *  *  -  - 
3   -  *  *  *  *  *  -  - 
4   -  -  -  *  *  *  -  - 
";
        Assert.AreEqual(map3, grid.DisplayMap());

        grid.RemoveRectangle(grid.FindRectangle(new Point(4, 3)));
        var map4 = @"   0  1  2  3  4  5  6  7  
0   -  -  -  -  -  *  *  * 
1   -  *  *  *  -  *  *  * 
2   -  *  *  *  -  -  -  - 
3   -  *  *  *  -  -  -  - 
4   -  -  -  -  -  -  -  - 
";
        Assert.AreEqual(map4, grid.DisplayMap());
    }
}
