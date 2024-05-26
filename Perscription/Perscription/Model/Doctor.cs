using System.ComponentModel.DataAnnotations;

namespace Perscription.Model;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    
    public virtual ICollection<PerscriptionC> PerscriptionCs { get; set; }
}