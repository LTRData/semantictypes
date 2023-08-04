namespace SemanticTypes;

public class SemanticDecimalTypeQualifiedByValue<Q> : SemanticTypeQualifiedByValue<decimal, Q, SemanticDecimalTypeQualifiedByValue<Q>>
{
    public SemanticDecimalTypeQualifiedByValue(decimal value, Q qualifyingValue)
        : base(value, qualifyingValue)
    {
    }

    // -----------------------------------------------------------------
    // Binary operator 

    public static SemanticDecimalTypeQualifiedByValue<Q> operator +(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c)
    {
        return EitherNullOrDifferentQualifyingValue(b,c)
            ? null
            : new SemanticDecimalTypeQualifiedByValue<Q>(b.Value + c.Value, b.QualifyingValue);
    }

    public static SemanticDecimalTypeQualifiedByValue<Q> operator -(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c)
    {
        return EitherNullOrDifferentQualifyingValue(b, c)
            ? null
            : new SemanticDecimalTypeQualifiedByValue<Q>(b.Value - c.Value, b.QualifyingValue);
    }

    public static SemanticDecimalTypeQualifiedByValue<Q> operator *(decimal b, SemanticDecimalTypeQualifiedByValue<Q> c) => c == null ? null : new SemanticDecimalTypeQualifiedByValue<Q>(b * c.Value, c.QualifyingValue);

    public static SemanticDecimalTypeQualifiedByValue<Q> operator *(SemanticDecimalTypeQualifiedByValue<Q> c, decimal b) => c == null ? null : new SemanticDecimalTypeQualifiedByValue<Q>(b * c.Value, c.QualifyingValue);

    public static SemanticDecimalTypeQualifiedByValue<Q> operator /(SemanticDecimalTypeQualifiedByValue<Q> c, decimal b) => c == null ? null : new SemanticDecimalTypeQualifiedByValue<Q>(c.Value / b, c.QualifyingValue);

    // For example, 10 meters / 5 meters = 2, because 2 * 5 meters = 10 meters.
    public static decimal? operator /(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c) => EitherNullOrDifferentQualifyingValue(b, c) ? null : b.Value / c.Value;

    // -----------------------------------------------------------------
    // Unary operator 

    public static SemanticDecimalTypeQualifiedByValue<Q> operator -(SemanticDecimalTypeQualifiedByValue<Q> c) => c == null ? null : new SemanticDecimalTypeQualifiedByValue<Q>(-1 * c.Value, c.QualifyingValue);

    // -----------------------------------------------------------------
    // Comparisons

    public static bool operator <(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c) => !EitherNullOrDifferentQualifyingValue(b, c) && b.Value < c.Value;

    public static bool operator <=(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c) => !EitherNullOrDifferentQualifyingValue(b, c) && b.Value <= c.Value;

    public static bool operator >(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c) => !EitherNullOrDifferentQualifyingValue(b, c) && b.Value > c.Value;

    public static bool operator >=(SemanticDecimalTypeQualifiedByValue<Q> b, SemanticDecimalTypeQualifiedByValue<Q> c) => !EitherNullOrDifferentQualifyingValue(b, c) && b.Value >= c.Value;
}
