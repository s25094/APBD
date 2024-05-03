using System.ComponentModel.DataAnnotations;
namespace Warehouse.Model;
public class Order
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required,Range(1, 999)]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}
