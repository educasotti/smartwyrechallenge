using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore: IProductDataStore
{
    public List<Product> Products { get; set; }
    public ProductDataStore()
    {
        Products = new List<Product> { new Product { Id = 1, Identifier = "product-1", Price = 100, Uom = "unit", SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedRateRebate } };
    }
    public Product GetProduct(string productIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return Products.FirstOrDefault(x => x.Identifier == productIdentifier);
    }
}
