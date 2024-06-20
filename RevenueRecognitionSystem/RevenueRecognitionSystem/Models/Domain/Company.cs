using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    
    public void setCompany(string companyName, string email, string phone, string address)
    {
        CompanyName = companyName;
        setClient(email, phone, address);
    }
}