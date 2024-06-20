using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Model;

public class Software
{
    [Key]
    public int SoftwareId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string SoftwareName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string SoftwareDescrition { get; set; }
    
    [Required]
    public double Version { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Category { get; set; }
    
    [Required]
    [Column(TypeName="money")]
    public decimal FullPrice { get; set; }
    
    [Required]
    public int SubscritionPrice { get; set; }
    
    public virtual ICollection<SoftwareOrder> SoftwareOrders { get; set; }
}