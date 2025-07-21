using HugeProject.Models;

namespace HugeProject.Services.Interfaces;

public interface IProductService : IBaseService<Product>
{
    IQueryable<Product> GetUnderPrice(int maxPrice);
    IQueryable<Product> GetAbovePrice(int minPrice);

    IQueryable<Product> GetProductByCategory(int categorId);
    IQueryable<Product> GetProductByBrand(int brandId);
    IQueryable<Product> GetProductByName(string name);

    Task<int?> CheckProductStock(int productId);
    Task<Product?> UpdateStock(int productId, int newStock);
    Task<Product?> IncreaseStock(int productId, int amountToIncrease);
    Task<Product?> DecreaseStock(int productId, int amountToDecrease); 
}