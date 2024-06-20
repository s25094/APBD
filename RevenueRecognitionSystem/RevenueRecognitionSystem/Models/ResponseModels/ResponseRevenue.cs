using System.Collections;

namespace RevenueRecognitionSystem.Models.ResponseModels;

public class ResponseRevenue
{
    public string companyRevenue { get; set; }
    public Hashtable productsRevenues { get; set; }
    
}