using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    public class RebateServiceTests
    {
        private readonly Mock<IRebateDataStore> _rebateDataStoreMock;
        private readonly Mock<IProductDataStore> _productDataStoreMock;
        private readonly List<IIncentiveCalculator> _incentiveCalculators;
        private readonly RebateService _rebateService;

        public RebateServiceTests()
        {
            _rebateDataStoreMock = new Mock<IRebateDataStore>();
            _productDataStoreMock = new Mock<IProductDataStore>();
            _incentiveCalculators = new List<IIncentiveCalculator>
            {
                new FixedCashAmountCalculator(),
                new FixedRateRebateCalculator(),
                new AmountPerUomCalculator()
            };
            _rebateService = new RebateService(_rebateDataStoreMock.Object, _productDataStoreMock.Object, _incentiveCalculators);
        }

        [Fact]
        public void Calculate_ShouldReturnSuccess_ForValidFixedCashAmount()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate-1", ProductIdentifier = "product-1", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            _rebateDataStoreMock.Verify(r => r.StoreCalculationResult(rebate, 100), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldReturnFailure_ForInvalidRebate()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "invalid-rebate", ProductIdentifier = "product-1", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns((Rebate)null);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnFailure_ForInvalidProduct()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate-1", ProductIdentifier = "invalid-product", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns((Product)null);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnSuccess_ForValidFixedRateRebate()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate-1", ProductIdentifier = "product-1", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            _rebateDataStoreMock.Verify(r => r.StoreCalculationResult(rebate, 100), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldReturnSuccess_ForValidAmountPerUom()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 10 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate-1", ProductIdentifier = "product-1", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            _rebateDataStoreMock.Verify(r => r.StoreCalculationResult(rebate, 100), Times.Once);
        }
    }
}