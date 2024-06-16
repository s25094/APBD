using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RevenueRecognitionSystem.Context;

namespace RevenueRecognitionSystem.Model;

public class UpfrontContract
{
    private RevenueRecognitionContext _dbContext = new RevenueRecognitionContext();
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContractId { get; set; }
    
    [Required]
    public int isSubscription { get; set; }
    
    [ForeignKey("ClientId")]
    public Client Client { get; set; }
    
    public int isSigned { get; set; }
    [ForeignKey("SoftwareId")]
    public Software Software { get; set; }
    
    [Required]
    public int Updates { get; set; }
    
    
    public virtual ICollection<Discount> Discounts { get; set; }
    [Column(TypeName="money")]
    public decimal Price { get; set; }
    
    public virtual ICollection<Payment> Payments { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int isPaid { get; set; }
    
    [Required] 
    [MaxLength(100)]
    public DateTime StartDate {get; set; }
    
    [Required] 
    [MaxLength(100)]
    public DateTime EndDate {get; set; }

    public void updatePrice()
    {
        decimal amount = Price;
        if (Updates > 1)
        {
            amount += 1000 * Updates;
        }

        if (isSubscription.Equals(1))
        {
            amount = amount/12;
        }

        if (Discounts.Count > 0)
        {
            int max = Discounts.MaxBy(d => d.Percentage).Percentage;
            Console.WriteLine(max);
            amount = amount - (amount * (max / 100));
        }

        if (_dbContext.UpfrontContracts.Any(c => c.Client.ClientId.Equals(Client.ClientId) && c.Status.Equals("Active")))
        {
            decimal additionals = 5;
            amount = amount - (amount * (additionals/100));
        }

        Price = amount;
    }
    
}