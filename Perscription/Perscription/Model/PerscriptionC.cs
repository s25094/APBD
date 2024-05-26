using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Perscription.Model;

public class PerscriptionC
{
    [Key]
    public int IdPerscription { get; set; }

    [Required]
    public DateTime? Date { get; set; }
    
    [Required]
    public DateTime? DueDate { get; set; }
    
    [ForeignKey("IdDoctor")]
    public virtual Doctor Doctor { get; set; }
    
    [ForeignKey("IdPatient")]
    public virtual Patient Patient { get; set; }
    
    public virtual ICollection<Perscription_Medicament> Perscription_Medicament { get; set; }
}