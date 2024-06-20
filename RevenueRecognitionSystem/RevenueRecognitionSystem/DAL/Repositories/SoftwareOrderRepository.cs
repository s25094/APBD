using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public class SoftwareOrderRepository : ISoftwareOrderRepository
{
    private RevenueRecognitionContext _dbContext;
    public SoftwareOrderRepository(RevenueRecognitionContext recognitionContext)
    {
        _dbContext = recognitionContext;
    }
    public async Task<int> AddNewSoftwareOrder(SoftwareOrder softwareOrder,
        CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(softwareOrder, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return softwareOrder.OrderId;
    }

    public async Task<SoftwareOrder?> GetOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _dbContext.SoftwareOrders.FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));
    }
    
    public async Task<UpfrontContract?> GetUpfrontContractOrdersAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _dbContext.UpfrontContracts.FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));
    }
    
    public async Task<Subscription?> GetSubscriptionsAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .Include(s => s.Payments)
            .FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));
    }

    public async Task<IEnumerable<UpfrontContract>> GetAllUpfrontContract()
    {
        return await _dbContext.UpfrontContracts
            .ToListAsync();
    }
    

    public async Task<IEnumerable<Subscription>> GetAllSubscriptions()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }
    
    public async Task<IEnumerable<Subscription?>> GetAllSubscriptionsByProductID(int productID,CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions.Where(s => s.Software.SoftwareId.Equals(productID)).ToListAsync();
    }
    
    public async Task<IEnumerable<UpfrontContract?>> GetAllContractsByProductID(int productID,CancellationToken cancellationToken)
    {
        return await _dbContext.UpfrontContracts.Where(s => s.Software.SoftwareId.Equals(productID)).ToListAsync();
    }

    public async Task<IEnumerable<SoftwareOrder>> GetAllOrders(CancellationToken cancellationToken)
    {
        return await _dbContext.SoftwareOrders.ToListAsync();
    }
}