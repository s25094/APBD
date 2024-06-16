using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;

namespace RevenueRecognitionSystem.Model;

public class Contract
{
    private RevenueRecognitionContext _dbContext = new RevenueRecognitionContext();

    public int IdClient { get; set; }
    public int isCompanyClient { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SoftwareId { get; set; }
    public int isSubscription { get; set; }
    public int Updates { get; set; }
    
    public Collection<int> discountsID { get; set; }

    public Client findClient()
    {
        if (isCompanyClient == 1)
        {
            return _dbContext.Companies
                .FirstOrDefault(client => client.ClientId.Equals(IdClient));
        }

        return _dbContext.Individuals
            .FirstOrDefault(client => client.ClientId.Equals(IdClient));

    }

    public bool validateDate()
    {
        if (StartDate.AddDays(30) < EndDate || StartDate.AddDays(3) > EndDate)
        {
            return true;
        }

        return false;
    }

    public bool checkSubscription()
    {
        return _dbContext.UpfrontContracts
            .Any(c => c.Client.ClientId.Equals(IdClient)
                      && c.Status.Equals("Active")
                      && c.isSubscription == 1
            );
    }

    public Software findSoftware()
    {
       return _dbContext.Softwares.FirstOrDefault(s => s.SoftwareId.Equals(SoftwareId));
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
            return "Customer already has active subscription";
        }

        if (findSoftware() == null)
        {
            return "There is no software for this id number";
        }

        return "ok";
    }

}