# Semantic Types

Semantic Types help make domain concepts explicit in C# and VB.NET. For example, an `EmailAddress` parameter communicates more than a `string` parameter, and a constructor can validate input before it enters the rest of an application. Distinct types also let the compiler catch accidental substitutions when APIs use those concrete types.

## About this LTRData fork

This is LTRData's fork of [Matt Perdeck's Semantic Types](https://github.com/mperdeck/semantictypes). The LTRData changes in August 2023 converted the projects to SDK-style builds, added multiple target frameworks and `LTRData.*` package metadata, and updated the C# source and xUnit tests. The checked-in package version is `2.0.1`.

The original [Introducing Semantic Types in .Net](http://www.codeproject.com/Articles/860646/Introducing-Semantic-Types-in-Net) article explains the design. Use the source and examples in this fork for current signatures.

## Packages and frameworks

| Package / project | Contents |
|---|---|
| `LTRData.SemanticTypes` | Base classes for validated values, comparable and non-comparable wrappers, and numeric values with qualifiers. |
| `LTRData.SemanticTypes.Examples` | Examples including `EmailAddress`, `BirthDate`, `NonNullable<T>`, `Id<Q>` and currency-qualified `Amount`. |
| `LTRData.SemanticTypes.TypeSystem.Physics` | Example quantity types: distance, area, volume, weight and energy, with conversions and arithmetic. |

All three libraries target **.NET Framework 3.5 and 4.0, .NET Standard 2.0, and .NET 6**. Package names use the `LTRData.` prefix; namespaces remain under `SemanticTypes`.

Install the core library:

```sh
dotnet add package LTRData.SemanticTypes
```

Or use the Visual Studio Package Manager Console:

```powershell
Install-Package LTRData.SemanticTypes
```

The separate examples and physics projects reference the core library. Their source is available in [SemanticTypes.Examples](https://github.com/LTRData/semantictypes/tree/master/SemanticTypes.Examples) and [SemanticTypes.TypeSystem.Physics](https://github.com/LTRData/semantictypes/tree/master/SemanticTypes.TypeSystem.Physics).

## Define a semantic type

Inherit from `SemanticType<T>` when the underlying type implements `IComparable<T>`. Its constructor takes a validation predicate and the value. The base class rejects null values and throws `ArgumentException` when the predicate returns false; it exposes the accepted value through `Value` and supplies equality, comparison and `ToString()` behavior.

Here is the original email-validation example, updated to the current constructor signature:

```csharp
using System.Text.RegularExpressions;
using SemanticTypes;

public class EmailAddress : SemanticType<string>
{
	public static bool IsValid(string value)
	{
		return (Regex.IsMatch(value,
						@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
						@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
						RegexOptions.IgnoreCase));
	}

	public EmailAddress(string emailAddress) : base(IsValid, emailAddress) { }
}
```


Use it as follows:

```csharp
bool valid = EmailAddress.IsValid("test@corp.com"); // true
var emailAddress = new EmailAddress("test@corp.com");
string text = emailAddress.Value;

bool invalid = EmailAddress.IsValid("not a valid email address"); // false
// new EmailAddress("not a valid email address") throws ArgumentException.
```

This regex illustrates constructor validation; accepting the pattern does not verify that an address exists or can receive mail. Choose validation rules appropriate to your domain.

For an underlying type with neither `IComparable<T>` nor `IComparable`, use `UncomparableSemanticType<T>`. Values qualified by another value, such as a currency code, use `SemanticTypeQualifiedByValue<T, Q, S>`; `SemanticDecimalTypeQualifiedByValue<Q>` adds decimal arithmetic. See the [Amount example](https://github.com/LTRData/semantictypes/blob/master/SemanticTypes.Examples/SemanticTypeQualifiedByValueExamples/Amount.cs).

### Behavior to account for

- Constructor validation applies to the supplied predicate. Wrapping a mutable reference does not make the underlying object immutable.
- In `SemanticTypeBase<T>`, `Equals(object)` checks the concrete runtime type, but typed `Equals` and `==` compare underlying values without the same type check. `CompareTo` also compares underlying values. Do not assume these base APIs enforce separation between different derived types sharing the same `T`.
- The inherited `IXmlSerializable` implementation is limited, including no support for string values. `ReadXml` changes `Value` without rerunning constructor validation; it is not a general-purpose validated serialization contract.

## Build and tests

Use a modern .NET SDK capable of targeting .NET 6. For a core-library build from the repository root:

```sh
dotnet build SemanticTypes/SemanticTypes.csproj -c Debug -f net6.0
```

The xUnit project targets `net48` and `net6.0`. With the .NET 6 runtime available, run its .NET 6 target:

```sh
dotnet test SemanticTypes.Test/SemanticTypes.Test.csproj -c Debug -f net6.0
```

Running the .NET Framework test target requires Windows and .NET Framework 4.8. Full multi-target builds also need the relevant .NET Framework reference assemblies. Test package references use floating versions.

Release builds automatically generate packages, using `LocalNuGetPath` as the package output path. Current package metadata comes from [Directory.Build.props](https://github.com/LTRData/semantictypes/blob/master/Directory.Build.props); the retained `.nuspec` and `.nuspec.template` files describe older upstream packaging.

## License

[LICENSE.txt](https://github.com/LTRData/semantictypes/blob/master/LICENSE.txt) directs readers to the original repository for license details.
