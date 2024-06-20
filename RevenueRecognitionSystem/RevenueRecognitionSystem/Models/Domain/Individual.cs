using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Model;



public class Individual : Client
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(100)] 
    public string PESEL { get; set; }

    public void setIndividual(string firstName, string lastName, string email, string phone, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        
        this.setClient(email, phone, address);
    }

}