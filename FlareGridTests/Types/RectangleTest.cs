namespace FlareGridTests;
using FlareGridApp.Types;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json.Linq;

[TestClass]
public class RectangleTest
{
    [TestMethod]
    public void Test_Contains()
    {
        Point startPoint = new Point(1, 2);
        Point endPoint = new Point(4, 5);
        Rectangle rect = new Rectangle(startPoint, endPoint);
        Assert.IsTrue(rect.Contains(startPoint));
        Assert.IsTrue(rect.Contains(endPoint));
        Assert.IsTrue(rect.Contains(new Point(1, 2)));
        Assert.IsTrue(rect.Contains(new Point(1, 3)));
        Assert.IsTrue(rect.Contains(new Point(4, 4)));
        Assert.IsTrue(rect.Contains(new Point(3, 3)));
        Assert.IsTrue(rect.Contains(new Point(4, 5)));

        Assert.IsFalse(rect.Contains(new Point(0, 0)));
        Assert.IsFalse(rect.Contains(new Point(5, 5)));
    }

    [TestMethod]
    public void Test_IsValidRectangle()
    {
        Point startPoint = new Point(1, 2);
        Point endPoint = new Point(4, 5);
        Rectangle rect = new Rectangle(startPoint, endPoint);

        Assert.IsTrue(rect.IsValidRectangle(7, 7));
        // position is from 0, so a minimum grid to contain [4,5] should [5,6]
        Assert.IsTrue(rect.IsValidRectangle(5, 6));
        Assert.IsFalse(rect.IsValidRectangle(4, 6));
        Assert.IsFalse(rect.IsValidRectangle(5, 4));

        Assert.IsTrue(Rectangle.IsValidRectangle(7, 7, rect));
        Assert.IsTrue(Rectangle.IsValidRectangle(5, 6, rect));
        Assert.IsFalse(Rectangle.IsValidRectangle(4, 6, rect));
        Assert.IsFalse(Rectangle.IsValidRectangle(5, 4, rect));
    }
}
