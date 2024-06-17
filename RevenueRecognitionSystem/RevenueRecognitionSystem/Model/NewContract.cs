using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;

namespace RevenueRecognitionSystem.Model;

public class NewContract : NewSale
{
    private RevenueRecognitionContext _dbContext = new RevenueRecognitionContext();

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Updates { get; set; }
    
    
    public bool validateDate()
    {
        if (StartDate.AddDays(30) < EndDate || StartDate.AddDays(3) > EndDate)
        {
            return true;
        }
        return false;
    }

    
    public string validateContract()
    {
        if (findClient() == null)
        {
            return "No customer was found";
        }

        if (validateDate())
        {
            return "Contract duration has to be at least 3 days and max 30 days";
        }

        if (checkSubscription())
        {
            return "Customer already has active subscription/contract";
        }

        if (findSoftware() == null)
        {
            return "There is no software for this id number";
        }

        return "ok";
    }

}