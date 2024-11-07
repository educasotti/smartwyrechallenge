using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Calculators
{
    public class FixedRateRebateCalculatorTests
    {
        private readonly FixedRateRebateCalculator _calculator;

        public FixedRateRebateCalculatorTests()
        {
            _calculator = new FixedRateRebateCalculator();
        }

        [Fact]
        public void CanCalculate_ShouldReturnTrue_ForFixedRateRebateIncentiveType()
        {
            // Arrange
            var incentiveType = IncentiveType.FixedRateRebate;

            // Act
            var result = _calculator.CanCalculate(incentiveType);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanCalculate_ShouldReturnFalse_ForNonFixedRateRebateIncentiveType()
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
            var rebate = new Rebate { Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
            var request = new CalculateRebateRequest { Volume = 10 };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(100, result);
        }

        [Fact]
        public void Calculate_ShouldReturnZero_ForUnsupportedIncentive()
        {
            // Arrange
            var rebate = new Rebate { Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 100 };
            var request = new CalculateRebateRequest { Volume = 10 };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Calculate_ShouldReturnZero_ForZeroRebatePercentage()
        {
            // Arrange
            var rebate = new Rebate { Percentage = 0 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
            var request = new CalculateRebateRequest { Volume = 10 };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Calculate_ShouldReturnZero_ForZeroProductPrice()
        {
            // Arrange
            var rebate = new Rebate { Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 0 };
            var request = new CalculateRebateRequest { Volume = 10 };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Calculate_ShouldReturnZero_ForZeroRequestVolume()
        {
            // Arrange
            var rebate = new Rebate { Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
            var request = new CalculateRebateRequest { Volume = 0 };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
