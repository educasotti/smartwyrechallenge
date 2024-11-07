using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public class FixedRateRebateCalculator : IIncentiveCalculator
    {
        public bool CanCalculate(IncentiveType incentiveType) => incentiveType == IncentiveType.FixedRateRebate;

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) || rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                return 0;
            }

            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}