using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Model;

public abstract class Client 
{
    [Key]
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

    public void setClient(string email, string phone, string address)
    {
        Email = email;
        Phone = phone;
        Address = address;
    }
    
}