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

namespace AsyncAwait.Task1.CancellationTokens;

    internal class Program
    {
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        /// <summary>
        /// The Main method should not be changed at all.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
            Console.WriteLine("Calculating the sum of integers from 0 to N.");
            Console.WriteLine("Use 'q' key to exit...");
            Console.WriteLine();

            Console.WriteLine("Enter N: ");

            var input = Console.ReadLine();
            while (input.Trim().ToUpper() != "Q")
            {
                if (int.TryParse(input, out var n))
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = new CancellationTokenSource();

                    CalculateSumAsync(n, _cancellationTokenSource.Token);
                }
                else
                {
                    Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                }

                Console.WriteLine("Enter N: ");
                input = Console.ReadLine();
            }

            _cancellationTokenSource.Dispose();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static async void CalculateSumAsync(int n, CancellationToken token)
        {
            try
            {
                Console.WriteLine($"The task for {n} started... Enter another value to cancel.");

                var sum = await Calculator.CalculateAsync(n, token);
                // todo: add code to process cancellation and uncomment this line    
                // Console.WriteLine($"Sum for {n} cancelled...");
                if (!token.IsCancellationRequested)
                {
                    Console.WriteLine($"Sum for {n} = {sum}.");
                }
                else
                {
                    Console.WriteLine($"Sum calculation for {n} was cancelled.");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Calculation for {n} was cancelled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while calculating sum for {n}: {ex.Message}");
            }
        }
    }
