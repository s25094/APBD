using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Perscription.Model;

public class NewDose
{
    public int IdMedicament { get; set; }
    [AllowNull]
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}