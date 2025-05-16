using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services;

public class PrivacyDataService : IPrivacyDataService
{
    /// <summary>
    /// Retrieves privacy data asynchronously with cancellation support.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to monitor for cancellation requests.</param>
    /// <returns>The privacy policy string.</returns>
    public async Task<string> GetPrivacyDataAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        return "This Policy describes how async/await processes your personal data, " +
               "but it may not address all possible data processing scenarios.";
    }
}