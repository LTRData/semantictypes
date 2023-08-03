using System;
using System.Collections.Generic;
using SemanticTypes.SemanticTypeExamples;
using Xunit;

namespace SemanticTypes.Test;


public class BirthDateTests
{
    [Fact]
    public void IsValid_BirthDateInFuture_False()
    {
        var birthDateDT = DateTime.Now + new TimeSpan(1, 0, 0, 0);
        Assert.False(BirthDate.IsValid(birthDateDT));
    }

    [Fact]
    public void IsValid_BirthDateTooManyYearsAgo_False()
    {
        var birthDateDT = DateTime.Now - new TimeSpan(130 * 365, 0, 0, 0);
        Assert.False(BirthDate.IsValid(birthDateDT));
    }

    [Fact]
    public void IsValid_BirthDate30YearsAgo_True()
    {
        var birthDateDT = DateTime.Now - new TimeSpan(30 * 365, 0, 0, 0);
        Assert.True(BirthDate.IsValid(birthDateDT));
    }

    [Fact]
    public void BirthDateInFuture_Exception()
        => Assert.Throws<ArgumentException>(() =>
        {
            var birthDateDT = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            var birthDate = new BirthDate(birthDateDT);
        });

    [Fact]
    public void BirthDateTooManyYearsAgo_Exception()
        => Assert.Throws<ArgumentException>(() =>
        {
            var birthDateDT = DateTime.Now - new TimeSpan(130 * 365, 0, 0, 0);
            var birthDate = new BirthDate(birthDateDT);
        });

    [Fact]
    public void BirthDate30YearsAgo_Succeeds()
    {
        var birthDateDT = DateTime.Now - new TimeSpan(30 * 365, 0, 0, 0);
        var birthDate = new BirthDate(birthDateDT);

        Assert.Equal(birthDateDT, birthDate.Value); 
    }

    [Fact]
    public void Equals_ValuesEqual_True()
    {
        var now = DateTime.Now;
        var birthDateDT = new BirthDate(now - new TimeSpan(30 * 365, 0, 0, 0));
        var birthDateDT2 = new BirthDate(now - new TimeSpan(30 * 365, 0, 0, 0));
        Assert.True(birthDateDT.Equals(birthDateDT2));
    }

    [Fact]
    public void Equals_CompareWithNull_False()
    {
        var birthDateDT = new BirthDate(DateTime.Now - new TimeSpan(30 * 365, 0, 0, 0));
        Assert.False(birthDateDT.Equals(null));
    }

    [Fact]
    public void Equals_ValuesNotEqual_False()
    {
        var now = DateTime.Now;
        var birthDateDT = new BirthDate(now - new TimeSpan(30 * 365, 0, 0, 0));
        var birthDateDT2 = new BirthDate(now - new TimeSpan(31 * 365, 0, 0, 0));
        Assert.False(birthDateDT.Equals(birthDateDT2));
    }

    [Fact]
    public void EqualsOperator_ValuesEqual_True()
    {
        var now = DateTime.Now;
        var birthDateDT = new BirthDate(now - new TimeSpan(30 * 365, 0, 0, 0));
        var birthDateDT2 = new BirthDate(now - new TimeSpan(30 * 365, 0, 0, 0));
        Assert.True(birthDateDT == birthDateDT2);
    }

    [Fact]
    public void EqualsOperator_CompareWithNullRhs_False()
    {
        var birthDateDT = new BirthDate(DateTime.Now - new TimeSpan(30 * 365, 0, 0, 0));
        Assert.False(birthDateDT == null);
    }

    [Fact]
    public void EqualsOperator_CompareWithNullLhs_False()
    {
        var birthDateDT = new BirthDate(DateTime.Now - new TimeSpan(30 * 365, 0, 0, 0));
        Assert.False(null == birthDateDT);
    }

    [Fact]
    public void EqualsOperator_ValuesNotEqual_False()
    {
        var now = DateTime.Now;
        var birthDateDT = new BirthDate(now - new TimeSpan(30 * 365, 0, 0, 0));
        var birthDateDT2 = new BirthDate(now - new TimeSpan(31 * 365, 0, 0, 0));
        Assert.False(birthDateDT == birthDateDT2);
    }

    [Fact]
    public void IsValidTwice_Tomorrow_False()
    {
        var dt = DateTime.Now + new TimeSpan(1, 0, 0, 0); // Tomorrow
        bool isValid = BirthDate.IsValid(dt);
        Assert.False(isValid);

        BirthDate birthDate;
        try { birthDate = new BirthDate(dt); }
        catch
        { 
        }

        bool isValid2 = BirthDate.IsValid(dt);
        Assert.False(isValid2);
    }

    [Fact]
    public void Constructor_Tomorrow_ThrowsException()
        => Assert.Throws<ArgumentException>(() =>
        {
            var dt = DateTime.Now + new TimeSpan(1, 0, 0, 0); // Tomorrow
            BirthDate birthDate = new BirthDate(dt);
        });

    [Fact]
    public void Test_IComparable()
    {
        BirthDate b = new BirthDate(new DateTime(2014, 02, 15));
        BirthDate a = new BirthDate(new DateTime(2014, 01, 19));
        BirthDate c = new BirthDate(new DateTime(2014, 05, 01));

        List<BirthDate> es = new List<BirthDate>(new[] { a, b, c });
        es.Sort();

        Assert.Equal(es[0], a);
        Assert.Equal(es[1], b);
        Assert.Equal(es[2], c);
    }

    [Fact]
    public void Test_ToString()
    {
        DateTime dt = new DateTime(2014, 02, 15);
        string dtAsString = dt.ToString();

        BirthDate a = new BirthDate(dt);
        string aAsString = a.ToString();

        Assert.Equal(dtAsString, aAsString);
    }
}
