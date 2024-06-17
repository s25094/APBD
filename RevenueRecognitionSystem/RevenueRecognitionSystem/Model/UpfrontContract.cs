using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using RevenueRecognitionSystem.Context;

namespace RevenueRecognitionSystem.Model;

public class UpfrontContract : SoftwareOrder
{
    private RevenueRecognitionContext _dbContext = new RevenueRecognitionContext();
    
    public int isSigned { get; set; }
    [Required]
    public int Updates { get; set; }
    [Required] 
    [MaxLength(100)]
    public DateTime StartDate {get; set; }
    
    [Required] 
    [MaxLength(100)]
    public DateTime EndDate {get; set; }

    public void AddUpdates()
    {
        CalculatePrice();
        Price *= Updates;
    }
}