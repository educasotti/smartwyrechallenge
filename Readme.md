# Smartwyre Developer Test - Guideline

## How to Run:

1. In the Smartwyre.DeveloperTest.Runner directory, 
2. Open a new command prompt in PowerShell
3. Run the following commands:
   1. `dotnet build`
   2. `dotnet run`

After started, the application will prompt for the name of the Rebate. 
A single rebate identified as **rebate-1** was mocked in order to return a successful message.

**If other identifier is informed, the return should be Failed**.

The same procedure is expected when the application prompts for the name of the Product. 

Once informed the identifier and the volume, the calculation process will start.
  
## How to Test
1. In the root of the solution, open a new command prompt in PowerShell
2. Run the following command: `dotnet test`



## How to Create a New Incentive Type:

1.	Define the new incentive type in the IncentiveType enum.
2.	Create a new class that implements the IIncentiveCalculator interface.
3.	Implement the CanCalculate and Calculate methods in the new class.
4.	Register the new calculator in the dependency injection container.


