namespace SemanticTypes.TypeSystem.Physics;

/// <summary>
/// Represents an area
/// </summary>
public class Area : SemanticDoubleType<Area>
{
    /// <summary>
    /// Creates an area
    /// </summary>
    /// <param name="value">
    /// Size in square meters
    /// </param>
    public Area(double value): base(value) {}

    public static Volume operator *(Area b, Distance c) => (b == null) || (c == null) ? null : new Volume(b.Value * c.Value);

    public static Volume operator *(Distance b, Area c) => (b == null) || (c == null) ? null : new Volume(b.Value * c.Value);

    public static Distance operator /(Area b, Distance c) => (b == null) || (c == null) ? null : new Distance(b.Value / c.Value);
}
