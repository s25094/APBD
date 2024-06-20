using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public interface ISoftwareRepository
{
    Task<Software?> GetSoftwareAsync(int idSoftware, CancellationToken cancellationToken);
    Task<IEnumerable<Software>> GetAllSoftwares(CancellationToken cancellationToken);
}