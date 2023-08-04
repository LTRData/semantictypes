using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SemanticTypes;

public interface IValue<T>
{
    T Value { get; }
}

/// <summary>
/// Internal base type of a semantic type. Do not inherit from this directly, it is infrastructure only.
/// </summary>
/// <typeparam name="T">
/// Type of the underlying value. If your semantic type is "EmailAddress" with an underlying value of type string,
/// then pass "string" here.
/// </typeparam>
public abstract class SemanticTypeBase<T> : IEquatable<SemanticTypeBase<T>>, IValue<T>, IXmlSerializable
{
    /// <summary>
    /// The Value property allows you to get the underlying value of a semantic type
    /// (but not to set it).
    /// </summary>
    /// <example>
    /// <![CDATA[
    /// public class EmailAddress : SemanticType<string, EmailAddress>
    /// {
    ///    ...
    /// }
    /// 
    /// var validEmailAddress = new EmailAddress("kjones@megacorp.com");
    /// string emailAddressString = validEmailAddress.Value; // "kjones@megacorp.com"
    /// ]]>
    /// </example>
    public T Value { get; private set; }

    protected SemanticTypeBase(Func<T, bool> isValidLambda, T value)
    {
        if ((object)value == null)
        {
            throw new ArgumentException(string.Format("Trying to use null as the value of a {0}", GetType()));
        }

        if ((isValidLambda != null) && !isValidLambda(value))
        {
            throw new ArgumentException(string.Format("Trying to set a {0} to {1} which is invalid", GetType(), value));
        }

        Value = value;
    }

    public override bool Equals(object obj) =>
        //Check for null and compare run-time types. 
        obj != null && obj.GetType() == GetType() && Value.Equals(((SemanticTypeBase<T>)obj).Value);

    public override int GetHashCode() => Value.GetHashCode();

    public virtual bool Equals(SemanticTypeBase<T> other) => other != null && Value.Equals(other.Value);

    public static bool operator ==(SemanticTypeBase<T> a, SemanticTypeBase<T> b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        // Have to cast to object, otherwise you recursively call this == operator.
        if (EitherNull(a, b))
        {
            return false;
        }

        // Return true if the fields match:
        return a.Equals(b);
    }

    public static bool operator !=(SemanticTypeBase<T> a, SemanticTypeBase<T> b) => !(a == b);

    protected static bool EitherNull(SemanticTypeBase<T> a, SemanticTypeBase<T> b) => (a is null) || (b is null);

    public override string ToString() => Value.ToString();

    public XmlSchema GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        reader.MoveToContent();
        if (typeof(T) == typeof(byte[]))
        {
            var buffer = Convert.FromBase64String(reader.ReadElementContentAsString());
            Value = (T)(object)buffer;
        }
        else if (typeof(T) == typeof(ushort)
             || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(byte)
            || typeof(T) == typeof(sbyte))
        {
            Value = (T)(object)reader.ReadElementContentAsInt();
        }
        else if (typeof(T) == typeof(uint))
        {
            Value = (T)(object)(uint)reader.ReadElementContentAsLong();
        }
        else if (typeof(T) == typeof(long))
        {
            Value = (T)(object)reader.ReadElementContentAsLong();
        }
        else
        {
            throw new NotImplementedException($"Write of {typeof(T)}");
        }

    }

    public void WriteXml(XmlWriter writer)
    {
        if (typeof(T) == typeof(byte[]))
        {
            var buffer = (byte[])(object)Value;
            writer.WriteBase64(buffer, 0, buffer.Length);
        }
        else if (typeof(T) == typeof(ushort)
             || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(byte)
            || typeof(T) == typeof(sbyte))
        {
            writer.WriteValue((int)(object)Value);
        }
        else if (typeof(T) == typeof(uint))
        {
            writer.WriteValue((long)(uint)(object)Value);
        }
        else if (typeof(T) == typeof(long))
        {
            writer.WriteValue((long)(object)Value);
        }
        else
        {
            throw new NotImplementedException($"Write of {typeof(T)}");
        }
    }
}
