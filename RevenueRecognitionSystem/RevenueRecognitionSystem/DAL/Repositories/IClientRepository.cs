using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public interface IClientRepository
{ 
    Task<Client?> GetClientAsync(int idClient, CancellationToken cancellationToken);
    
    Task<int> AddNewClientAsync(Client client, CancellationToken cancellationToken);

    Task<Client> SaveChangesAsync(Client client, CancellationToken cancellationToken);

    Task<Individual?> GetIndividualClientAsync(int clientId, CancellationToken cancellationToken);
    Task<Company?> GetCompanyClientAsync(int clientId, CancellationToken cancellationToken);
    
}