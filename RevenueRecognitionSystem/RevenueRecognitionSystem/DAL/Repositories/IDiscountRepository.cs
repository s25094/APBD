using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public interface IDiscountRepository
{
    ICollection<Discount> GetDiscountAsync(CancellationToken cancellationToken);
}