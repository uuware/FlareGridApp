namespace FlareGridTests;
using FlareGridApp.Types;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json.Linq;

[TestClass]
public class PointTest
{
    [TestMethod]
    public void Test_PointIsValid()
    {
        Point point = new Point(4, 5);
        Assert.IsTrue(point.IsValidPoint(5, 6));
        Assert.IsFalse(point.IsValidPoint(4, 6));
        Assert.IsFalse(point.IsValidPoint(5, 5));
        Assert.IsFalse(point.IsValidPoint(5, 5));

        Assert.AreEqual(true, Point.IsValidPoint(5, 6, point));
        Assert.IsFalse(Point.IsValidPoint(4, 6, point));
        Assert.IsFalse(Point.IsValidPoint(5, 5, point));
        Assert.IsFalse(Point.IsValidPoint(5, 5, point));
    }

    [TestMethod]
    public void Test_Parse()
    {
        Point? point = Point.Parse("5,6");
        Assert.AreEqual(true, point != null);
        Assert.AreEqual(5, point?.X);
        Assert.AreEqual(6, point?.Y);
        Assert.IsTrue(point?.IsValidPoint(6, 7));
        Assert.IsFalse(point?.IsValidPoint(5, 6));

        Assert.IsTrue(Point.IsValidPoint(6, 7, point!));
        Assert.IsFalse(Point.IsValidPoint(5, 6, point!));
    }

    [TestMethod]
    public void Test_ParseNotGood()
    {
        Point? point = Point.Parse("5,");
        Assert.IsTrue(point == null);
    }
}
