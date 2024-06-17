using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Model;

public class Subscription : SoftwareOrder
{
    
    [Required]
    [MaxLength(100)]
    public  string SubscriptionName { get; set; }

    [Required]
    [MaxLength(100)]
    public  string RenewalPeriod { get; set; }
    
    [Required]
    public  int QuantityOfRenewalPeriod { get; set; }
    
    
    public void CalculateSubscription()
    {
        CalculatePrice();
        Price /= QuantityOfRenewalPeriod;
        if (RenewalPeriod.Equals('m'))
        {
            Price /= 12;
        }

        Price = System.Math.Round(Price, 2);
    }

}