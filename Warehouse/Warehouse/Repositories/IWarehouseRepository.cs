using Warehouse.Model;
namespace Warehouse.Repositories;

public interface IWarehouseRepository
{
    int CreateOrder(Order order);
    double CheckIfProductExists(Order order);
    bool CheckIfWarehouseExists(int WarehouseID);
    int CheckIfOrderExists(Order order);
    
    int IsOrderFinalized(int OrderId);
    void UpdateOrder(int OrderId);

}