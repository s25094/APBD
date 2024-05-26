using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Perscription.Model;

[PrimaryKey(nameof(IdMedicament), nameof(IdPerscription))]
public class Perscription_Medicament
{
    
    [ForeignKey("IdMedicament")]
    public int IdMedicament { get; set; }
    
    [ForeignKey("IdPerscription")]
    public int IdPerscription { get; set; }
    
    //[ForeignKey("IdMedicament")]
    //public virtual Medicament Medicament { get; set; }
    
    //[ForeignKey("IdPerscription")]
    //public virtual PerscriptionC PerscriptionC { get; set; }
    
    [AllowNull]
    public int? Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}