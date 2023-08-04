namespace SemanticTypes.TypeSystem.Physics;

/// <summary>
/// Encapsulates volume
/// </summary>
public class Volume : SemanticDoubleType<Volume>
{
    /// <summary>
    /// Creates a volume
    /// </summary>
    /// <param name="value">
    /// Size in cubic meters
    /// </param>
    public Volume(double value) : base(value) { }

    public static Area operator /(Volume b, Distance c) => (b == null) || (c == null) ? null : new Area(b.Value / c.Value);

    public static Distance operator /(Volume b, Area c) => (b == null) || (c == null) ? null : new Distance(b.Value / c.Value);
}
