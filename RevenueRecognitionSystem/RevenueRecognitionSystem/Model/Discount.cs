using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Model;

public class Discount
{
    [Key] 
    [Required] 
    public int DiscountId { get; set; }
    
    [Required] 
    public int Percentage {get; set; }
    
    [Required] 
    [MaxLength(100)]
    public DateTime StartDate {get; set; }
    
    [Required] 
    [MaxLength(100)]
    public DateTime EndDate {get; set; }
    
    public virtual ICollection<UpfrontContract> UpfrontContracts { get; set; }
    
}