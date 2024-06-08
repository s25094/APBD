using System.ComponentModel.DataAnnotations;

namespace Boats.Model;

public class NewReservation
{
    [Required]
    public int IdClient { get; set; }
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    [Required]
    public int IdBoatStandard { get; set; }
    [Required]
    public int NumOfBoats { get; set; }
    
    
}