using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Model;


public class Company : Client
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }
    
    [Required]
    [MaxLength(100)] 
    public string KRS { get; set; }
}