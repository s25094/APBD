using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.Services;

public interface ISoftwareOrdeService
{
    Task<int> CreateNewContractAsync(ContractRequest contractRequest, CancellationToken cancellationToken);
    Task<int> CreateNewSubscriptionAsync(SubscriptionRequest subscriptionRequest, CancellationToken cancellationToken);
}