using System.ComponentModel.DataAnnotations;

namespace Boats.Model;

public class BoatStandard
{
    [Key] 
    [Required]
    public int IdBoatStandard { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public int Level { get; set; }
}