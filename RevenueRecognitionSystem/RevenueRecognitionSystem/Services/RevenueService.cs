using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using RevenueRecognitionSystem.DAL.Repositories;
using RevenueRecognitionSystem.Model;
using RevenueRecognitionSystem.Models.ResponseModels;

namespace RevenueRecognitionSystem.Services;

public class RevenueService : IRevenueService
{
    private readonly ISoftwareOrderRepository _softwareOrderRepository;
    private readonly ISoftwareRepository _isSoftwareRepository;
    private readonly IPaymentRepository _paymentRepository;
    public RevenueService(ISoftwareOrderRepository softwareOrderRepository, ISoftwareRepository isSoftwareRepository, IPaymentRepository paymentRepository)
    {
        _softwareOrderRepository = softwareOrderRepository;
        _isSoftwareRepository = isSoftwareRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<ResponseRevenue> CalculatePredictedRevenue(CancellationToken cancellationToken)
    {
        var allUpfrontOrders = await _softwareOrderRepository.GetAllUpfrontContract();
        var allSubscriptions = await _softwareOrderRepository.GetAllSubscriptions();

        var allUpfrontqQantity = allUpfrontOrders.ToList().Sum(c => c.Price);
        var allSubscriptionQantity = allSubscriptions.ToList().Sum(c => c.Price * c.QuantityOfRenewalPeriod);
        var totalRevenueForCompany = allUpfrontqQantity + allSubscriptionQantity;
        var allSoftwares = await _isSoftwareRepository.GetAllSoftwares(cancellationToken);
        
        Hashtable ht = new Hashtable();
        foreach (Software s in allSoftwares)
        {
            var contracts = await _softwareOrderRepository
                .GetAllContractsByProductID(s.SoftwareId, cancellationToken);
            var subscriptions = await _softwareOrderRepository
                .GetAllSubscriptionsByProductID(s.SoftwareId, cancellationToken);
            decimal amount = 0;
            amount += contracts.ToList().Sum(s => s.Price);
            amount += subscriptions.ToList().Sum(s => s.Price * s.QuantityOfRenewalPeriod);
            ht.Add(s.SoftwareName, System.Math.Round(amount,2));
        }

        var responseRevenue = new ResponseRevenue
        {
            companyRevenue = "Company revenue: " + System.Math.Round(totalRevenueForCompany,2),
            productsRevenues = ht
        };
        
        return responseRevenue;
    }
    
    
    public async Task<ResponseRevenue> CalculateCurrentRevenue(CancellationToken cancellationToken)
    {

        var softwares = _isSoftwareRepository.GetAllSoftwares(cancellationToken);
        Hashtable ht = new Hashtable();
        decimal total = 0;
        foreach (Software s in softwares.Result)
        {
            decimal payments = 0;
            foreach (SoftwareOrder so in s.SoftwareOrders)
            { payments += so.Payments.Sum(p => p.paymentAmount);
            }
            
            ht.Add(s.SoftwareName, payments);
            total += payments;
        }
        
        
        var responseRevenue = new ResponseRevenue
        {
            companyRevenue = "Company revenue: " + System.Math.Round(total,2),
            productsRevenues = ht
        };
        
        return responseRevenue;
    }
    
    
}