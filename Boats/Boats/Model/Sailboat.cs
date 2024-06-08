using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boats.Model;

public class Sailboat
{
    [Key] 
    [Required]
    public int IdSailboat { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public int Capacity { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    
    [ForeignKey("IdBoatStandard")]
    public virtual BoatStandard BoatStandard { get; set; }
    
    [Required]
    public float Price { get; set; }
    
  
    public  ICollection<Sailboat_Reservation> Sailboat_Reservation { get; set; }
}