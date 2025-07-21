using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces;

public interface IProductService : IBaseService<Product>
{
    //Price Filters
    IQueryable<Product> GetUnderPrice(int maxPrice);
    IQueryable<Product> GetAbovePrice(int minPrice);

    //Searches
    List<ProductDto> GetProductByCategoryDto(int categoryId);
    IQueryable<Product> GetProductByCategory(int categorId);
    IQueryable<Product> GetProductByBrand(int brandId);
    IQueryable<Product> GetProductByName(string name);

    //Stock Processes
    Task<int?> CheckProductStock(int productId);
    Task<Product?> UpdateStock(int productId, int newStock);
    Task<Product?> IncreaseStock(int productId, int amountToIncrease);
    Task<Product?> DecreaseStock(int productId, int amountToDecrease); 
}