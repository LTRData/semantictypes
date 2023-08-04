namespace SemanticTypes.SemanticTypeExamples;

public class NonNullable<T> : UncomparableSemanticType<T>
{
    public static implicit operator T(NonNullable<T> t) => t.Value;

    // force dev to know that we're going to NonNullable
    public static explicit operator NonNullable<T>(T t) => new(t);

    public static bool IsValid(T value) => value != null;

    public NonNullable(T value)
        : base(IsValid, value)
    {
    }
}
