using RevenueRecognitionSystem.DAL.Repositories;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.Services;

public class SoftwareOrderService : ISoftwareOrdeService 
{
    private readonly ISoftwareOrderRepository _softwareOrderRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ISoftwareRepository _iSoftwareRepository;
    private readonly IDiscountRepository _discountRepository;
    public SoftwareOrderService(ISoftwareOrderRepository softwareOrderRepository, IClientRepository clientRepository
    , ISoftwareRepository iSoftwareRepository, IDiscountRepository discountRepository)
    {
        _softwareOrderRepository = softwareOrderRepository;
        _clientRepository = clientRepository;
        _iSoftwareRepository = iSoftwareRepository;
        _discountRepository = discountRepository;
    }
    
    public async Task<int>  CreateNewContractAsync(ContractRequest contractRequest,
        CancellationToken cancellationToken)
    {

        if (contractRequest.EndDate.Subtract(contractRequest.StartDate).Days < 3
            || contractRequest.EndDate.Subtract(contractRequest.StartDate).Days > 30)
        {
            throw new Exception("Contract duration should be between 3 and 30 days!");
        }

        var client = await _clientRepository.GetClientAsync(contractRequest.IdClient, cancellationToken);
        if (client == null)
        {
            throw new Exception("Client does not exist!");
        }
        
        foreach ( SoftwareOrder s in client.SoftwareOrders){
            if (s.Status.Equals("Active"))
            {
                throw new Exception("Customer has an active contract"); 
            }
        }

        var software = await _iSoftwareRepository.GetSoftwareAsync(contractRequest.SoftwareId, cancellationToken);
        
        if (software == null)
        {
            throw new Exception("Software does not exist!");
        }

        if (contractRequest.Updates < 1 || contractRequest.Updates > 3)
        {
            throw new Exception("Customer can choose between 1-3 software updates!");
        }
        
        var discounts = _discountRepository.GetDiscountAsync(cancellationToken);

        var max_discount = 0;
        if (discounts != null)
        {
            max_discount = discounts.MaxBy(d => d.Percentage).Percentage;
        }
        if (client.SoftwareOrders.Any())
        {
            max_discount += 5;
        }
        
        var price = (software.FullPrice - software.FullPrice*max_discount/100) + contractRequest.Updates*1000;

        var upfronContract = new UpfrontContract
        {
            Client = client,
            Software = software,
            isSigned = 0,
            isPaid = 0,
            Status = "NEW",
            StartDate = contractRequest.StartDate,
            EndDate = contractRequest.EndDate,
            Updates = contractRequest.Updates,
            Discounts = discounts,
            Price = price
        };
        
        return await _softwareOrderRepository.AddNewSoftwareOrder(upfronContract, cancellationToken);
    }


    public async Task<int> CreateNewSubscriptionAsync(SubscriptionRequest subscriptionRequest,
        CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientAsync(subscriptionRequest.IdClient, cancellationToken);
        if (client == null)
        {
            throw new Exception("Client does not exist!");
        }
        
        foreach ( SoftwareOrder s in client.SoftwareOrders){
            if (s.Status.Equals("Active"))
            {
                throw new Exception("Customer has an active contract"); 
            }
        }

        var software = await _iSoftwareRepository.GetSoftwareAsync(subscriptionRequest.SoftwareId, cancellationToken);
        
        if (software == null)
        {
            throw new Exception("Software does not exist!");
        }

        if (subscriptionRequest.RenewalPeriod.Equals("m")
            && (subscriptionRequest.QuantityOfRenewalPeriod < 1 || subscriptionRequest.QuantityOfRenewalPeriod > 24))
        {
            throw new Exception("Monthly subscription should last from 1 to 24 months!");
        }
        
        if (!subscriptionRequest.RenewalPeriod.Equals("m")
            && (subscriptionRequest.QuantityOfRenewalPeriod < 1 || subscriptionRequest.QuantityOfRenewalPeriod > 2))
        {
            throw new Exception("Monthly subscription should last from 1 to 2 years!");
        }
        
        var discounts = _discountRepository.GetDiscountAsync(cancellationToken);

        var max_discount = 0;
        if (discounts != null)
        {
            max_discount = discounts.MaxBy(d => d.Percentage).Percentage;
        }

        if (client.SoftwareOrders.Any())
        {
            max_discount += 5;
        }

        var price = software.FullPrice / subscriptionRequest.QuantityOfRenewalPeriod;
        price =  System.Math.Round(price - price * max_discount / 100,2);

        var subscriptionContract = new Subscription
        {
            Client = client,
            Software = software,
            isPaid = 1,
            Status = "Active",
            Discounts = discounts,
            Price = price, 
            RenewalPeriod = subscriptionRequest.RenewalPeriod,
            QuantityOfRenewalPeriod = subscriptionRequest.QuantityOfRenewalPeriod, 
            SubscriptionName = subscriptionRequest.SubscriptionName
        };
        
        
        return await _softwareOrderRepository.AddNewSoftwareOrder(subscriptionContract, cancellationToken);
    }

}