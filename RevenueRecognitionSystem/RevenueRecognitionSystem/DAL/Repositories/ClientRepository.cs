using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly RevenueRecognitionContext _dbContent;

    public ClientRepository(RevenueRecognitionContext revenueRecognitionContext)
    {
        _dbContent = revenueRecognitionContext;
    }

    public Task<Client?> GetClientAsync(int idClient, CancellationToken cancellationToken)
    {
        return _dbContent.Clients
            .Include(o => o.SoftwareOrders)
            .ThenInclude(c => c.Payments)
            .FirstOrDefaultAsync(c => c.ClientId == idClient, cancellationToken);
    }

    public async Task<int> AddNewClientAsync(Client client, CancellationToken cancellationToken)
    {
        await _dbContent.AddAsync(client, cancellationToken);
        await _dbContent.SaveChangesAsync(cancellationToken);

        return client.ClientId;
    }

    public async Task<Client> SaveChangesAsync(Client client, CancellationToken cancellationToken)
    {
        await _dbContent.SaveChangesAsync(cancellationToken);
        return client;
    }

    public async Task<Individual?> GetIndividualClientAsync(int clientId, CancellationToken cancellationToken)
    {
        return await _dbContent.Individuals.FirstOrDefaultAsync(i => i.ClientId.Equals(clientId));
    }

    public async Task<Company?> GetCompanyClientAsync(int clientId, CancellationToken cancellationToken)
    {
        return await _dbContent.Companies.FirstOrDefaultAsync(c => c.ClientId.Equals(clientId));
    }

}