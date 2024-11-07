using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Calculators
{
    public class FixedCashAmountCalculatorTests
    {
        private readonly FixedCashAmountCalculator _calculator;

        public FixedCashAmountCalculatorTests()
        {
            _calculator = new FixedCashAmountCalculator();
        }

        [Fact]
        public void CanCalculate_ShouldReturnTrue_ForFixedCashAmountIncentiveType()
        {
            // Arrange
            var incentiveType = IncentiveType.FixedCashAmount;

            // Act
            var result = _calculator.CanCalculate(incentiveType);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanCalculate_ShouldReturnFalse_ForNonFixedCashAmountIncentiveType()
        {
            // Arrange
            var incentiveType = IncentiveType.FixedRateRebate;

            // Act
            var result = _calculator.CanCalculate(incentiveType);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Calculate_ShouldReturnCorrectAmount_ForValidInput()
        {
            // Arrange
            var rebate = new Rebate { Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest();

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(100, result);
        }

        [Fact]
        public void Calculate_ShouldReturnZero_ForUnsupportedIncentive()
        {
            // Arrange
            var rebate = new Rebate { Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
            var request = new CalculateRebateRequest();

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
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest();

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(0, result);
        }
    }
}