using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.DAL.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly RevenueRecognitionContext _dbContent;

    public DiscountRepository(RevenueRecognitionContext revenueRecognitionContext)
    {
        _dbContent = revenueRecognitionContext;
    }

    public  ICollection<Discount> GetDiscountAsync(CancellationToken cancellationToken)
    {
        return _dbContent.Discounts.Where(d => d.StartDate <= DateTime.Now && DateTime.Now <= d.EndDate).ToList();
    }
}