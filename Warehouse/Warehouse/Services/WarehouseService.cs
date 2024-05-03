using Warehouse.Model;
using Warehouse.Repositories;
namespace Warehouse.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    public int CreateOrder(Order order){
       return _warehouseRepository.CreateOrder(order);
    }
}