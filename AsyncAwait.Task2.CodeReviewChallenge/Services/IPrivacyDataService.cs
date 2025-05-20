using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services;

public interface IPrivacyDataService
{
    /// <summary>
    /// Retrieves privacy data asynchronously with support for cancellation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The privacy data as a string.</returns>
    Task<string> GetPrivacyDataAsync(CancellationToken cancellationToken);
}