using System.ComponentModel.DataAnnotations;

namespace Perscription.Model;

public class NewPerscription
{

    public int IdPatient { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    

    public DateTime? BirthDate { get; set; }
    
    public ICollection<NewDose> NewDoses { get; set; }
    
    
    [Required]
    public DateTime? Date { get; set; }
    
    [Required]
    public DateTime? DueDate { get; set; }
    
    public int IdDoctor { get; set; }

    
    
}