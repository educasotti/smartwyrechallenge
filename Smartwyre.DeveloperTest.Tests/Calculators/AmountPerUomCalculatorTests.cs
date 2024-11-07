using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Calculators;

public class AmountPerUomCalculatorTests
{
    private readonly AmountPerUomCalculator _calculator;

    public AmountPerUomCalculatorTests()
    {
        _calculator = new AmountPerUomCalculator();
    }

    [Fact]
    public void CanCalculate_ShouldReturnTrue_ForAmountPerUomIncentiveType()
    {
        // Arrange
        var incentiveType = IncentiveType.AmountPerUom;

        // Act
        var result = _calculator.CanCalculate(incentiveType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanCalculate_ShouldReturnFalse_ForNonAmountPerUomIncentiveType()
    {
        // Arrange
        var incentiveType = IncentiveType.FixedCashAmount;

        // Act
        var result = _calculator.CanCalculate(incentiveType);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturnCorrectAmount_ForValidInput()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        Assert.Equal(50, result);
    }

    [Fact]
    public void Calculate_ShouldReturnZero_ForUnsupportedIncentive()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Calculate_ShouldReturnZero_ForZeroRebateAmount()
    {
        // Arrange
        var rebate = new Rebate { Amount = 0 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Calculate_ShouldReturnZero_ForZeroRequestVolume()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 0 };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        Assert.Equal(0, result);
    }
}