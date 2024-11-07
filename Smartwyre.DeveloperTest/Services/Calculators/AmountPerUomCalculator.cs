using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public class AmountPerUomCalculator : IIncentiveCalculator
    {
        public bool CanCalculate(IncentiveType incentiveType) => incentiveType == IncentiveType.AmountPerUom;

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) || rebate.Amount == 0 || request.Volume == 0)
            {
                return 0;
            }

            return rebate.Amount * request.Volume;
        }
    }
}