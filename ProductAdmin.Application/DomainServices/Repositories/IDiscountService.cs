namespace ProductAdmin.Application.DomainServices.Repositories;

public interface IDiscountService
{
    ValueTask<int> GetDiscount(int productId);
}