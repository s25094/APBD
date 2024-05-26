using System.ComponentModel.DataAnnotations;

namespace Perscription.Model;

public class Perscription
{
    [Key]
    public int IdPerscription { get; set; }
}