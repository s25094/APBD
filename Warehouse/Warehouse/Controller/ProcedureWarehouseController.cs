using Warehouse.Model;
using Warehouse.Services;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Controller;

[Route("api/procedure/warehouse")]
[ApiController]
public class ProcedureWarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public ProcedureWarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        var newId = _warehouseService.CreateOrderByProcedure(order);
        return StatusCode(StatusCodes.Status201Created);
    }

}