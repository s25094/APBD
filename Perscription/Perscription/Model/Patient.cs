using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Perscription.Model;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    

    public DateTime? BirthDate { get; set; }
    
    public virtual ICollection<PerscriptionC> PerscriptionCs { get; set; }
    
    
    
}