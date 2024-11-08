using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public List<Rebate> Rebates { get; set; }

    public RebateDataStore()
    {
        Rebates = new List<Rebate> { new Rebate { Amount = 10, Identifier = "rebate-1", Incentive = IncentiveType.FixedRateRebate, Percentage = 10 } };
    }
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return Rebates.FirstOrDefault(x => x.Identifier == rebateIdentifier);
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}
