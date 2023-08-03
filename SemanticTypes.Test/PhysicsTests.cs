using SemanticTypes.TypeSystem.Physics;
using Xunit;

namespace SemanticTypes.Test;

#pragma warning disable IDE0059 // Unnecessary assignment of a value

public class PhysicsTests
{
    [Fact]
    public void VariousPhysicsTests()
    {
        var distance1m = new Distance(1); // 1 meter
        var distance1m_2 = Distance.FromMeters(1);
        var distance1609m = Distance.FromMeters(1609);
        var distance1mile = Distance.FromMiles(1);
        var distance6m = Distance.FromMeters(6);
        var distance4m = Distance.FromMeters(4);
        var distance2m = Distance.FromMeters(2);
        var distance4i = Distance.FromInches(4);
        var distance2i = Distance.FromInches(2);
        var distance8m2 = distance2m * distance4m;

        Assert.True(distance1mile > distance1m_2);
        Assert.True(distance1m == distance1m_2);
        Assert.True(distance1609m == distance1mile);
        Assert.True(distance1609m.Meters == 1609);
        Assert.True(distance6m == distance4m + distance2m);
        Assert.True(distance6m == 3 * distance2m);
        Assert.True(distance6m == distance2m * 3);
        Assert.True(distance2m == distance6m / 3);
        Assert.True(distance2m == -1* (distance4m - distance6m));

        Distance d = (distance4m + distance6m);

        Assert.True(distance6m == distance4m + distance2m);
        Assert.True(6 == (distance4m + distance2m).Meters);

        Distance distanceMinus2m = (distance4m - distance6m);
        Assert.True(distanceMinus2m.Meters == -2);
        Assert.True(distance2m == distance6m - distance4m);
        Assert.True(distance8m2.Value == 8);
    }
}
