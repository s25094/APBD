using System.Collections;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public interface ISoftwareOrderRepository
{
    Task<int> AddNewSoftwareOrder(SoftwareOrder softwareOrder, CancellationToken CancellationToken);
    Task<SoftwareOrder?> GetOrderAsync(int orderId, CancellationToken cancellationToken);
    Task<UpfrontContract?> GetUpfrontContractOrdersAsync(int orderId, CancellationToken cancellationToken);
    Task<Subscription?> GetSubscriptionsAsync(int orderId, CancellationToken cancellationToken);
    
    Task<IEnumerable<UpfrontContract>> GetAllUpfrontContract();
    Task<IEnumerable<Subscription>> GetAllSubscriptions();

    Task<IEnumerable<Subscription?>> GetAllSubscriptionsByProductID(int productID, CancellationToken cancellationToken);

    Task<IEnumerable<UpfrontContract?>> GetAllContractsByProductID(int productID, CancellationToken cancellationToken);

    Task<IEnumerable<SoftwareOrder>> GetAllOrders(CancellationToken cancellationToken);
}