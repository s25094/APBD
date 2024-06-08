using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Boats.Model;

namespace Boats.Model;

public class Reservation
{
    [Key] [Required] public int IdReservation { get; set; }

    [ForeignKey("IdClient")]
    public virtual Client Client { get; set; }
    
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    
    [ForeignKey("IdBoatStandard")]
    public virtual BoatStandard BoatStandard { get; set; }
    
    [Required]
    public int Capacity { get; set; }
    
    [Required]
    public int NumOfBoats { get; set; }
    
    [Required]
    public int Fulfilled { get; set; }
    
    [Required]
    public float Price { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string CancelReason { get; set; }
    
   
    public ICollection<Sailboat_Reservation> Sailboat_Reservation { get; set; }
    
    
 
}