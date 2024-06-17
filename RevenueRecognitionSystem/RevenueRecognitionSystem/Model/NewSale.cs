using RevenueRecognitionSystem.Context;

namespace RevenueRecognitionSystem.Model;

public abstract class NewSale
{
    private RevenueRecognitionContext _dbContext = new RevenueRecognitionContext();
    public int IdClient { get; set; }
    public int SoftwareId { get; set; }
    
    
    public Client findClient()
    {
        return _dbContext.Clients.FirstOrDefault(client => client.ClientId.Equals(IdClient));
    }
    
    public Software findSoftware()
    {
        return _dbContext.Softwares.FirstOrDefault(s => s.SoftwareId.Equals(SoftwareId));
    }
    
    public bool checkSubscription()
    {
        return _dbContext.SoftwareOrders
            .Any(c => c.Client.ClientId.Equals(IdClient)
                      && c.Status.Equals("Active")
            );
    }
}