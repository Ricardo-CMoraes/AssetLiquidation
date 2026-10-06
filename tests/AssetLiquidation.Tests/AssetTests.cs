using AssetLiquidation.Core;
using FluentAssertions;

namespace AssetLiquidation.Tests;

public class AssetTests
{
    [Fact]
    public void ProcessPayment_WithValidAmount_ShouldDeductFromCurrentAmount()
    {
        var initialAmount = 1000.00m;
        var paymentAmount = 300.00m;
        var asset = new Asset("20045789061", initialAmount);

        asset.ProcessPayment(paymentAmount);

        asset.CurrentAmount.Should().Be(700.00m);
        asset.Status.Should().Be(AssetStatus.Active);
    }

    [Fact]
    public void ProcessPayment_AmountGreaterThanCurrentAmount_ShouldThrowArgumentException()
    {
        var initialAmount = 400.00m;
        var paymentAmount = 450.00m;
        var asset = new Asset("20045789061", initialAmount);

        Action act = () => asset.ProcessPayment(paymentAmount);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Payment amount must be greater than zero and less than or equal to the current amount.*");
    }

    [Fact]
    public void ProcessPayment_FullAmount_ShouldSetStatusToInactive()
    {
        var initialAmount = 800.00m;
        var paymentAmount = 800.00m;
        var asset = new Asset("20045789061", initialAmount);

        asset.ProcessPayment(paymentAmount);

        asset.CurrentAmount.Should().Be(0.00m);
        asset.Status.Should().Be(AssetStatus.Inactive);
    }

    [Fact]
    public void ProcessRefund_WithValidAmount_ShouldIncreaseCurrentAmount()
    {
        var initialAmount = 800.00m;
        var paymentAmount = 120.00m;
        var asset = new Asset("20045789061", initialAmount);

        asset.ProcessPayment(paymentAmount);

        asset.ProcessRefund(20.00m);

        asset.CurrentAmount.Should().Be(700.00m);
        asset.Status.Should().Be(AssetStatus.Active);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("123456789101112")]
    [InlineData("3173618729b")]
    [InlineData("")]
    public void CreateAsset_WithInvalidAssetId_ShouldThrowArgumentException(string invalidAssetId)
    {
        Action act = () => new Asset(invalidAssetId, 1000.00m);

        act.Should().Throw<ArgumentException>();
    }


    [Theory]
    [InlineData(0.00)]
    [InlineData(-50.00)]
    public void ProcessPayment_WithZeroOrNegativeAmount_ShouldThrowArgumentException(decimal amount)
    {
        var asset = new Asset("12345678910", 1000.00m);
        Action act = () => asset.ProcessPayment(amount);

        act.Should().Throw<ArgumentException>();
    }
}
