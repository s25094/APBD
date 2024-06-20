using RevenueRecognitionSystem.DAL.Repositories;
using RevenueRecognitionSystem.Model;
using RevenueRecognitionSystem.Models.ResponseModels;

namespace RevenueRecognitionSystem.Services;

public interface IClientService
{
    
    Task<Client> GetClientAsync(int idClient, CancellationToken cancellationToken);
    Task<int> AddNewIndividualClientAsync(IndividualRequest individualRequest, CancellationToken cancellationToken);
    Task<int> AddNewCompanyClientAsync(CompanyRequest companyRequest, CancellationToken cancellationToken);
    Task<Client> DeleteIndividualClientAsync(int idClient, CancellationToken cancellationToken);
    Task<ResponseIndividual> UpdateIndividualClientAsync(IndividualUpdateRequest individualRequest, CancellationToken cancellationToken);
    
    Task<ResponseCompany> UpdateCompanyClientAsync(CompanyUpdateRequest companyUpdateRequest, CancellationToken cancellationToken);

}