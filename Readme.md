# Smartwyre Developer Test - Main Documentation

How to Run:

1. In the Smartwyre.DeveloperTest.Runner directory, 
2. Open a new command prompt in PowerShell
3. Run the following commands:
   1. dotnet build
   2. dotnet run
  
How to Test
1. In the root of the solution, open a new command prompt in PowerShell
2. Run the following command:
   1. dotnet test


Post-Refactor Features:

- Testable
- Dependency Injection allowed
- Open the possibility of more Incentive Types
- Respecting the SOLID concepts:
   1.	Single Responsibility Principle (SRP): Each class has a single responsibility. The RebateService class is responsible for coordinating the rebate calculation, while individual calculators handle the logic for specific incentive types.
   2.	Open/Closed Principle (OCP): The RebateService class is open for extension but closed for modification. New incentive types can be added by creating new classes that implement the IIncentiveCalculator interface.
   3.	Liskov Substitution Principle (LSP): The IIncentiveCalculator interface ensures that any new incentive calculator can be used interchangeably.
   4.	Interface Segregation Principle (ISP): The IIncentiveCalculator interface is specific to the needs of the incentive calculation.
   5.	Dependency Inversion Principle (DIP): The RebateService class depends on abstractions (IRebateDataStore, IProductDataStore, and IIncentiveCalculator) rather than concrete implementations.

How to Create a New Incentive Type:
1.	Define the new incentive type in the IncentiveType enum.
2.	Create a new class that implements the IIncentiveCalculator interface.
3.	Implement the CanCalculate and Calculate methods in the new class.
4.	Register the new calculator in the dependency injection container.


