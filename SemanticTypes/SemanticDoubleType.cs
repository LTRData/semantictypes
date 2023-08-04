using System;

namespace SemanticTypes;

/// <summary>
/// When deriving from this type, pass in the derived type in parameter Q.
/// If you don't do that, then the static elements inside this class will be 
/// shared by all object of all classes derived from this base class.
/// </summary>
/// <typeparam name="Q"></typeparam>
public class SemanticDoubleType<Q> : SemanticType<double>
    where Q: class
{
    public SemanticDoubleType(double value)
        : base(null, value)
    {
    }

    // -----------------------------------------------------------------
    // Binary operator 

    public static Q operator +(SemanticDoubleType<Q> b, SemanticDoubleType<Q> c) => EitherNull(b, c) ? null : CreateQ(b.Value + c.Value);

    public static Q operator -(SemanticDoubleType<Q> b, SemanticDoubleType<Q> c) => EitherNull(b, c) ? null : CreateQ(b.Value - c.Value);

    public static Q operator *(double b, SemanticDoubleType<Q> c) => c == null ? null : CreateQ(b * c.Value);

    public static Q operator *(SemanticDoubleType<Q> c, double b) => c == null ? null : CreateQ(b * c.Value);

    public static Q operator /(SemanticDoubleType<Q> c, double b) => c == null ? null : CreateQ(c.Value / b);

    // -----------------------------------------------------------------
    // Unary operator 

    public static Q operator -(SemanticDoubleType<Q> c) => c == null ? null : CreateQ(-1 * c.Value);

    // -----------------------------------------------------------------
    // Comparisons

    public static bool operator <(SemanticDoubleType<Q> b, SemanticDoubleType<Q> c) => !EitherNull(b, c) && b.Value < c.Value;

    public static bool operator <=(SemanticDoubleType<Q> b, SemanticDoubleType<Q> c) => !EitherNull(b, c) && b.Value <= c.Value;

    public static bool operator >(SemanticDoubleType<Q> b, SemanticDoubleType<Q> c) => !EitherNull(b, c) && b.Value > c.Value;

    public static bool operator >=(SemanticDoubleType<Q> b, SemanticDoubleType<Q> c) => !EitherNull(b, c) && b.Value >= c.Value;

    private static Q CreateQ(double value)
    {
        object[] args = { value };
        object result = Activator.CreateInstance(typeof(Q), args);
        return (Q)result;
    }
}
