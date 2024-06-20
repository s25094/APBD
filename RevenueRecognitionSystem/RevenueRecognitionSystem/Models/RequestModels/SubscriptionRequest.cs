using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Model;

public class SubscriptionRequest
{
    public int IdClient { get; set; }
    public int SoftwareId { get; set; }
    public  string SubscriptionName { get; set; }
    public  string RenewalPeriod { get; set; }
    public  int QuantityOfRenewalPeriod { get; set; }
}