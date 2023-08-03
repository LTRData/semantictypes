using SemanticTypes.SemanticTypeQualifiedByValueExamples;
using Xunit;

namespace SemanticTypes.Test;

public class AmountTests
{
    [Fact]
    public void AllAmountTests()
    {
        Amount usd5Amount = new Amount(5, "USD");
        Amount usd5Amount2 = new Amount(5, "USD");
        Amount usd10Amount = new Amount(10, "USD");
        Amount eur5Amount = new Amount(5, "EUR");
        Amount eur2Amount = new Amount(2, "EUR");
        Amount eur3Amount = new Amount(3, "EUR");
        Amount nullAmount = null;

        Assert.Equal(eur5Amount, eur2Amount + eur3Amount);
        Assert.Equal(eur2Amount, eur5Amount - eur3Amount);
        Assert.Equal(usd5Amount, usd5Amount2);
        Assert.Equal(usd5Amount, usd10Amount / 2);
        Assert.Equal(usd10Amount, usd5Amount * 2);
        Assert.Equal(usd10Amount, 2 * usd5Amount);
        Assert.Equal(-1 * usd5Amount, -usd5Amount2);

        Assert.Null(eur2Amount + nullAmount);
        Assert.Null(nullAmount + eur2Amount);
        Assert.Null(nullAmount + nullAmount);

        // Amounts with different currencies
        Assert.Null(eur2Amount + usd5Amount);

        // -----------------------------------------------

        Assert.True(eur2Amount < eur3Amount);
        Assert.False(eur2Amount > eur3Amount);
        Assert.True(eur2Amount <= eur3Amount);
        Assert.False(eur2Amount >= eur3Amount);

        Assert.True(eur3Amount > eur2Amount);
        Assert.False(eur3Amount < eur2Amount);
        Assert.True(eur3Amount >= eur2Amount);
        Assert.False(eur3Amount <= eur2Amount);

        Assert.True(usd5Amount == usd5Amount2);
        Assert.False(eur3Amount == eur2Amount);
        Assert.True(eur3Amount != eur2Amount);
        Assert.False(usd5Amount != usd5Amount2);

        Assert.False(eur5Amount == usd5Amount);
        Assert.True(eur5Amount != usd5Amount);



    }
}
