using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boats.Model;

public class Client
{
    [Key] 
    [Required]
    public int IdClient { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public DateTime Birthday { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Pesel { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [ForeignKey("IdClientCategory")]
    public virtual ClientCategory ClientCategory { get; set; }
    
    public virtual ICollection<Reservation> Reservations { get; set; }
}