using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public interface IPaymentRepository
{
    Task<int> AddPaymentAsync(Payment payment, CancellationToken cancellationToken);
    
}