using Microsoft.AspNetCore.Http.HttpResults;
using RevenueRecognitionSystem.DAL.Repositories;
using RevenueRecognitionSystem.Model;
using RevenueRecognitionSystem.Models.ResponseModels;

namespace RevenueRecognitionSystem.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;


    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> GetClientAsync(int idClient, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientAsync(idClient, cancellationToken);
        if (client == null)
        {
            throw new BadHttpRequestException("Client not found");
        }

        return client;
    }

    public async Task<int> AddNewIndividualClientAsync(IndividualRequest individualRequest, CancellationToken cancellationToken)
    {
        var client = new Individual
        {
            FirstName = individualRequest.FirstName,
            LastName = individualRequest.LastName,
            PESEL = individualRequest.PESEL
        };
        
        client.setClient(individualRequest.Email, individualRequest.Phone, individualRequest.Address);
        return await _clientRepository.AddNewClientAsync(client,cancellationToken);
    }
    
    
    public async Task<int> AddNewCompanyClientAsync(CompanyRequest companyRequest, CancellationToken cancellationToken)
    {
        var client = new Company
        {
            KRS = companyRequest.KRS,
            CompanyName = companyRequest.CompanyName
        };
        
        client.setClient(companyRequest.Email, companyRequest.Phone, companyRequest.Address);
        return await _clientRepository.AddNewClientAsync(client,cancellationToken);
    }

    public async Task<Client> DeleteIndividualClientAsync(int idClient, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetIndividualClientAsync(idClient, cancellationToken);

        if (client == null)
        {
            throw new Exception("Individual client not found!");
        }
        client.setIndividual("", "", "", "", "");
        return await _clientRepository.SaveChangesAsync(client, cancellationToken);
    }

    public async Task<ResponseIndividual> UpdateIndividualClientAsync(IndividualUpdateRequest individualUpdateRequest, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetIndividualClientAsync(individualUpdateRequest.ClientId, cancellationToken);
        if (client == null)
        {
            throw new Exception("Individual client not found!");
        }
        client.setIndividual(individualUpdateRequest.FirstName, 
            individualUpdateRequest.LastName, individualUpdateRequest.Email, 
            individualUpdateRequest.Phone, individualUpdateRequest.Address);

        var updatedIndividual =  await _clientRepository.SaveChangesAsync(client, cancellationToken);
        return new ResponseIndividual
        {
            ClientId = updatedIndividual.ClientId,
            FirstName = client.FirstName, 
            LastName = client.LastName,
            PESEL = client.PESEL,
            Address = client.Address, 
            Email = client.Email, 
            Phone = client.Phone
        };
    }

    public async Task<ResponseCompany> UpdateCompanyClientAsync(CompanyUpdateRequest companyUpdateRequest, CancellationToken cancellationToken) {
        var client = await _clientRepository.GetCompanyClientAsync(companyUpdateRequest.ClientId, cancellationToken);
        if (client == null)
        {
            throw new Exception("Company client not found!");
        }
        client.setCompany(companyUpdateRequest.CompanyName, companyUpdateRequest.Email, 
            companyUpdateRequest.Phone, companyUpdateRequest.Address);

        var updatedCompany = await _clientRepository.SaveChangesAsync(client, cancellationToken);

        return new ResponseCompany
        {
            ClientId = updatedCompany.ClientId,
            CompanyName = client.CompanyName, 
            KRS = client.KRS, 
            Address = client.Address, 
            Email = client.Email, 
            Phone = client.Phone
        };

    }
}