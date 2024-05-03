using Warehouse.Model;
using Warehouse.Services;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Controller;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        var newId = _warehouseService.CreateOrder(order);
        return StatusCode(StatusCodes.Status201Created);
    }

}