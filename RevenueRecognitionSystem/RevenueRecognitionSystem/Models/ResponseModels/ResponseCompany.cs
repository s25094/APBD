using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models.ResponseModels;

public class ResponseCompany
{
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
    
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }
    
    [Required]
    [MaxLength(100)] 
    public string KRS { get; set; }
    
    public int ClientId { get; set; }
}