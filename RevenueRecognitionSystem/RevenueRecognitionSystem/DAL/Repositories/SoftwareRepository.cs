using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public class SoftwareRepository : ISoftwareRepository
{
    private RevenueRecognitionContext _dbContext;
    public SoftwareRepository(RevenueRecognitionContext recognitionContext)
    {
        _dbContext = recognitionContext;
    }

    public Task<Software?> GetSoftwareAsync(int idSoftware, CancellationToken cancellationToken)
    {
        return _dbContext.Softwares.FirstOrDefaultAsync(s => s.SoftwareId.Equals(idSoftware));
    }
    
    public async Task<IEnumerable<Software>> GetAllSoftwares(CancellationToken cancellationToken)
    {
        return  _dbContext.Softwares
            .Include( s=> s.SoftwareOrders)
            .ThenInclude(c => c.Payments);
    }
}