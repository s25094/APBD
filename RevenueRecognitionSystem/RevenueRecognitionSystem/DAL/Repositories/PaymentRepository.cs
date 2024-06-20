using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly RevenueRecognitionContext _dbContent;
    public PaymentRepository(RevenueRecognitionContext revenueRecognitionContext)
    {
        _dbContent = revenueRecognitionContext;
    }
    public async Task<int> AddPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        await _dbContent.AddAsync(payment, cancellationToken);
        await _dbContent.SaveChangesAsync(cancellationToken);

        return payment.PaymentId;
    }

  
}