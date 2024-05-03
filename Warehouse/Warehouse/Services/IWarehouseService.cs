using Warehouse.Model;
namespace Warehouse.Services;

public interface IWarehouseService{
    int CreateOrder(Order order);
}