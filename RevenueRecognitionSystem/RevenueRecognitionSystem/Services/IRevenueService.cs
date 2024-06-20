using RevenueRecognitionSystem.Models.ResponseModels;

namespace RevenueRecognitionSystem.Services;

public interface IRevenueService
{
    Task<ResponseRevenue> CalculatePredictedRevenue(CancellationToken cancellationToken);
    Task<ResponseRevenue> CalculateCurrentRevenue(CancellationToken cancellationToken);
}