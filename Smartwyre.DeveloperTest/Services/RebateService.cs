using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services
{
    public class RebateService : IRebateService
    {
        private readonly IRebateDataStore _rebateDataStore;
        private readonly IProductDataStore _productDataStore;
        private readonly IEnumerable<IIncentiveCalculator> _incentiveCalculators;

        public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IEnumerable<IIncentiveCalculator> incentiveCalculators)
        {
            _rebateDataStore = rebateDataStore;
            _productDataStore = productDataStore;
            _incentiveCalculators = incentiveCalculators;
        }

        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            var product = _productDataStore.GetProduct(request.ProductIdentifier);

            if (rebate == null || product == null)
            {
                return new CalculateRebateResult { Success = false };
            }

            var calculator = _incentiveCalculators.FirstOrDefault(c => c.CanCalculate(rebate.Incentive));

            if (calculator == null)
            {
                return new CalculateRebateResult { Success = false };
            }

            var rebateAmount = calculator.Calculate(rebate, product, request);

            if (rebateAmount > 0)
            {
                _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
                return new CalculateRebateResult { Success = true };
            }

            return new CalculateRebateResult { Success = false };
        }
    }
}