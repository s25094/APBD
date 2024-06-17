using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;

namespace RevenueRecognitionSystem.Model;

public abstract class SoftwareOrder
{
    private RevenueRecognitionContext _dbContext = new RevenueRecognitionContext();
    
    [Key]
    public int OrderId { get; set; }
    
    [ForeignKey("ClientId")]
    public Client Client { get; set; }
    
    [ForeignKey("SoftwareId")]
    public Software Software { get; set; }
    
    public int isPaid { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    [Column(TypeName="money")]
    public decimal Price { get; set; }
    
    public virtual ICollection<Payment> Payments { get; set; }


    public void CalculatePrice()
    {
        var discount = _dbContext.Discounts
            .Where(date => date.StartDate <= DateTime.Now && DateTime.Now >= date.StartDate)
            .OrderByDescending(d => d.Percentage)
            .First();
        
        if (discount != null)
        {
            Price -= Price * discount.Percentage/100;
        }

        if (_dbContext.SoftwareOrders.Any(o => o.Client.ClientId.Equals(Client.ClientId) && o.isPaid.Equals(1)))
        {
            Price -=  Price * (decimal)0.05;
        }
        
    }
}