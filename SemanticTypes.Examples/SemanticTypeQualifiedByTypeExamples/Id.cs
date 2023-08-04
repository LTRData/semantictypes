namespace SemanticTypes.SemanticTypeQualifiedByTypeExamples;

public class Id<Q> : SemanticType<int>
{
    public static bool IsValid(int value) => value >= 0;

    public Id(int id)
        : base(IsValid, id)
    {
    }
}
