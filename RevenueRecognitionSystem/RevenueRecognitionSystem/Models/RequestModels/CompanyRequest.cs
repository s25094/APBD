using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Model;

public class CompanyRequest
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }
    
    [Required]
    [MaxLength(100)] 
    public string KRS { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Address { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Phone]
    public string Phone { get; set; }
}