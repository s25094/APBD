using Warehouse.Model;
namespace Warehouse.Services;

public interface IWarehouseService{
    Task<int> CreateOrder(Order order);
    Task<int> CreateOrderByProcedure(Order order);

}