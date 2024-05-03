using Warehouse.Model;
namespace Warehouse.Repositories;

public interface IWarehouseRepository
{
    Task<int> CreateOrder(Order order);
    Task<double> CheckIfProductExists(Order order);
    Task<bool> CheckIfWarehouseExists(int WarehouseID);
    Task<int> CheckIfOrderExists(Order order);
    
    Task<int> IsOrderFinalized(int OrderId);
    Task UpdateOrder(int OrderId);

}