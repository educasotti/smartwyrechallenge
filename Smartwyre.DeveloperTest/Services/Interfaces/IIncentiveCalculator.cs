using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Interfaces
{
    public interface IIncentiveCalculator
    {
        bool CanCalculate(IncentiveType incentiveType);
        decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}