using RevenueRecognitionSystem.DAL.Repositories;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ISoftwareOrderRepository _softwareOrderRepository;

    public PaymentService(IPaymentRepository paymentRepository, ISoftwareOrderRepository softwareOrderRepository)
    {
        _paymentRepository = paymentRepository;
        _softwareOrderRepository = softwareOrderRepository;
    }

    public async Task<int> AddUpfrontPaymentAsync(PaymentRequest paymentRequest, CancellationToken cancellationToken)
    {
        var order = await _softwareOrderRepository
            .GetUpfrontContractOrdersAsync(paymentRequest.SoftwareOrderID, cancellationToken);
        
        if (order == null)
        {
            throw new BadHttpRequestException("Order not found");
        }
        
        if (order.isPaid.Equals(1))
        {
            throw new BadHttpRequestException("Order is already paid");
        }

        if (DateTime.Now > order.EndDate)
        {
            throw new BadHttpRequestException("Contract expired");
        }

        decimal all_payments = 0;
        if (order.Payments != null)
        {
            foreach (Payment p in order.Payments)
            {
                all_payments += p.paymentAmount;
            }
        }

        if (all_payments + paymentRequest.paymentAmount > order.Price)
        {
            throw new BadHttpRequestException("We cannot process this payment, the amount is too high!");
        }

        var newPayment = new Payment
        {
            SoftwareOrder = order,
            paymentAmount = paymentRequest.paymentAmount
        };
        
        if (order.Price.Equals(all_payments + paymentRequest.paymentAmount))
        {
            order.isSigned = 1;
            order.isPaid = 1;
            order.Status = "Active";
        }
        
        await _paymentRepository.AddPaymentAsync(newPayment, cancellationToken);
        return newPayment.PaymentId;
    }

    public async Task<int> AddSubscriptionPaymentAsync(PaymentRequest paymentRequest,
        CancellationToken cancellationToken)
    {
        var order = await _softwareOrderRepository
            .GetSubscriptionsAsync(paymentRequest.SoftwareOrderID, cancellationToken);
        
        if (order == null)
        {
            throw new BadHttpRequestException("Order not found");
        }
        
        decimal all_payments = 0;
        if (order.Payments != null)
        {
            foreach (Payment p in order.Payments)
            {
                all_payments += p.paymentAmount;
            }
        }
        if (all_payments + paymentRequest.paymentAmount > order.Price)
        {
            throw new BadHttpRequestException("We cannot process this payment, the amount is too high!");
        }
        
        var newPayment = new Payment
        {
            SoftwareOrder = order,
            paymentAmount = paymentRequest.paymentAmount
        };
        
        if (order.Price.Equals(all_payments + paymentRequest.paymentAmount))
        {
            order.isPaid = 1;
            order.Status = "Active";
        }
        
        await _paymentRepository.AddPaymentAsync(newPayment, cancellationToken);
        return newPayment.PaymentId;
        
    }

}