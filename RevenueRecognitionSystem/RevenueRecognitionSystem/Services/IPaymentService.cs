using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.Services;

public interface IPaymentService
{
    Task<int> AddUpfrontPaymentAsync(PaymentRequest payment, CancellationToken cancellationToken);
    Task<int> AddSubscriptionPaymentAsync(PaymentRequest subscriptionRequest, CancellationToken cancellationToken);
}