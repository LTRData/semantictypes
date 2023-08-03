using System;
using SemanticTypes.SemanticTypeExamples;
using Xunit;

namespace SemanticTypes.Test;


public class EmailAddressTests
{
    [Fact]
    public void SetToNull_Exception()
        => Assert.Throws<ArgumentException>(() =>
        {
            var emailAddress = new EmailAddress(null);
        });

    [Fact]
    public void SetToInvalid_Exception()
        => Assert.Throws<ArgumentException>(() =>
            {
                var emailAddress = new EmailAddress("Invalid email address");
            });

    [Fact]
    public void SetToValid_NoException()
    {
        var emailAddress = new EmailAddress("test12@test.com.au");
        Assert.Equal("test12@test.com.au", emailAddress.Value);
    }

    [Fact]
    public void Test_Implicit()
    {
        EmailAddress emailAddress = "example@contoso.com";
        Assert.Equal("example@contoso.com", emailAddress.Value);
    }

    [Fact]
    public void Test_Explicit()
    {
        EmailAddress emailAddress = new EmailAddress("example@contoso.com");

        string sAddress = (string)emailAddress;
        Assert.Equal("example@contoso.com", sAddress);
    }

    [Fact]
    public void SetToNull()
    {
        EmailAddress emailAddress = null;
        Assert.Null(emailAddress);
    }

    [Fact]
    public void SetToInvalidImplicit_Exception()
        => Assert.Throws<ArgumentException>(() =>
        {
            EmailAddress emailAddress = "Invalid email address";
        });
}
