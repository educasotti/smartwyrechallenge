using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public class FixedCashAmountCalculator : IIncentiveCalculator
    {
        public bool CanCalculate(IncentiveType incentiveType) => incentiveType == IncentiveType.FixedCashAmount;

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) || rebate.Amount == 0)
            {
                return 0;
            }

            return rebate.Amount;
        }
    }
}