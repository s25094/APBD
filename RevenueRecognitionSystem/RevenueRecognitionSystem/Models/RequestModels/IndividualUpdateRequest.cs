using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Model;

public class IndividualUpdateRequest 
{
    public int ClientId { get; set; }
   
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
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
}