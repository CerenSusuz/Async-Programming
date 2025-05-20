/*
* Study the code of this application to calculate the sum of integers from 0 to N, and then
* change the application code so that the following requirements are met:
* 1. The calculation must be performed asynchronously.
* 2. N is set by the user from the console. The user has the right to make a new boundary in the calculation process,
* which should lead to the restart of the calculation.
* 3. When restarting the calculation, the application should continue working without any failures.
*/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal class Program
{
    /// <summary>
    /// The Main method should not be changed at all.
    /// </summary>
    /// <param name="args"></param>
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
        Console.WriteLine("Calculating the sum of integers from 0 to N.");
        Console.WriteLine("Use 'q' key to exit...");
        Console.WriteLine();

        CancellationTokenSource activeTokenSource = null;
        Task lastCalculationTask = null;

        Console.WriteLine("Enter N:");
        var input = Console.ReadLine();

        while (input?.Trim().ToUpper() != "Q")
        {
            if (int.TryParse(input, out var n))
            {
                activeTokenSource?.Cancel();
                activeTokenSource?.Dispose();

                activeTokenSource = new CancellationTokenSource();

                if (lastCalculationTask != null)
                {
                    await lastCalculationTask;
                }

                lastCalculationTask = CalculateSumAsync(n, activeTokenSource.Token);
            }
            else
            {
                Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
            }

            Console.WriteLine("Enter N:");
            input = Console.ReadLine();
        }

        if (lastCalculationTask != null)
        {
            await lastCalculationTask;
        }

        activeTokenSource?.Dispose();
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

    /// <summary>
    /// Asynchronous calculation for sum from 0 to N.
    /// </summary>
    private static async Task CalculateSumAsync(int n, CancellationToken token)
    {
        try
        {
            Console.WriteLine($"The task for {n} started... Enter another value to cancel.");

            var sum = await Calculator.CalculateAsync(n, token);
            // todo: add code to process cancellation and uncomment this line    
            // Console.WriteLine($"Sum for {n} cancelled...");
            Console.WriteLine($"✅ Sum for {n} = {sum}.");
            Console.WriteLine("--- Calculation Complete ---");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"⚠️ Calculation for {n} was cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ An error occurred while calculating sum for {n}: {ex.Message}");
        }
    }
}
