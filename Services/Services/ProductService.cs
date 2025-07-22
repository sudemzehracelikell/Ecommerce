using System.Threading.Tasks;
using HugeProject.Models;
using HugeProject.Repository;

namespace HugeProject.Services;

public class ProductService : BaseService<Product>, Interfaces.IProductService
{
    public ProductService(IEnumarableRepository<Product> _enumRepository, IQueryableRepository<Product> _queryRepository)
     : base(_enumRepository, _queryRepository)
    { }


    //Price Filters
    public IQueryable<Product> GetUnderPrice(int maxPrice)
    {
        if (maxPrice <= 0)
        {
            throw new ArgumentException("Please enter a number greater than zero ");
        }
        return _queryRepository.FilterBy(p => p.Price <= maxPrice);
    }

    public IQueryable<Product> GetAbovePrice(int minPrice)
    {
        return _queryRepository.FilterBy(p => p.Price >= minPrice);
    }


    //Searches
    public IQueryable<Product> GetProductByCategory(int categorId)
    {
        return _queryRepository
                .GetWithIncludes(p => p.ProductCategory)
                .Where(p => p.ProductCategory.Any(pc => pc.CategoryId == categorId));
    }

    public IQueryable<Product> GetProductByBrand(int brandId)
    {
        return _queryRepository.FilterBy(p => p.BrandId == brandId);
    }

    public IQueryable<Product> GetProductByName(string name)
    {
        return _queryRepository.FilterBy(p => p.Name == name);
    }


    //Stock Processes
    public async Task<int?> CheckProductStock(int productId)
    {
        var p = await _enumRepository.GetById(productId);
        return p?.Count;
    }

    public async Task<Product?> UpdateStock(int productId, int newStock)
    {
        if (newStock < 0)
            throw new Exception("New Stock can't be negative");

        var p = await _enumRepository.GetById(productId);
        if (p != null)
        {
            p.Count = newStock;
            await _enumRepository.Update(p);
            return p;
        }
        return null;
    }

    public async Task<Product?> IncreaseStock(int productId, int amountToIncrease)
    {
        if (amountToIncrease < 0)
            throw new Exception("Amount to increase can't be negative");

        var p = await _enumRepository.GetById(productId);
        if (p != null)
        {
            p.Count += amountToIncrease;
            await _enumRepository.Update(p);
            return p;
        }
        return null;
    }

    public async Task<Product?> DecreaseStock(int productId, int amountToDecrease)
    {
        if (amountToDecrease > 0)
            throw new Exception("Amount to decrease can't be positive");

        var p = await _enumRepository.GetById(productId);
        if (p != null)
        {
            p.Count -= amountToDecrease;
            await _enumRepository.Update(p);
            return p;
        }
        return null;
    } 

}
