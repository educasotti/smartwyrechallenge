using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Threading;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = GetServiceProviders();
        var service = serviceProvider.GetService<IRebateService>();

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate-1",
            ProductIdentifier = "product-1",
            Volume = 100
        };

        var result = service.Calculate(request);

        Console.WriteLine(result.Success ? "Success" : "Failed" );
    }
    public static ServiceProvider GetServiceProviders()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IRebateDataStore, RebateDataStore>();
        services.AddScoped<IProductDataStore, ProductDataStore>();
        services.AddScoped<IIncentiveCalculator, FixedCashAmountCalculator>();
        services.AddScoped<IIncentiveCalculator, FixedRateRebateCalculator>();
        services.AddScoped<IIncentiveCalculator, AmountPerUomCalculator>();        
        services.AddScoped<IRebateService, RebateService>();
    }

    
}
