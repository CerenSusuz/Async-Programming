using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{
    internal static class Calculator
    {
        public static async Task<long> CalculateAsync(int n, CancellationToken token)
        {
            long sum = 0;

            for (var i = 0; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }

                sum += i;

                await Task.Delay(10, token);
            }

            return sum;
        }
    }
}