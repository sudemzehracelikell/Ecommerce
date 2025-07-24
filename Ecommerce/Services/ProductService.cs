
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class ProductService : BaseService<Product>, Interfaces.IProductService
{
    private readonly Context _context;

    public ProductService(
        IEnumarableRepository<Product> enumRepo,
        IQueryableRepository<Product> queryRepo,
        Context context)
        : base(enumRepo, queryRepo)
    {
        _context = context;
    }
    //Price Filters
    public IQueryable<Product> GetUnderPrice(int maxPrice)
    {
        if (maxPrice <= 0)
        {
            throw new ArgumentException("Please enter a number greater than zero ");
        }
        return _queryRepository.FilterBy(p => p.BasePrice <= maxPrice);
    }

    public IQueryable<Product> GetAbovePrice(int minPrice)
    {
        return _queryRepository.FilterBy(p => p.BasePrice >= minPrice);
    }


    //Searches
    public List<ProductDto> GetProductByCategoryDto(int categoryId)
    {
        var products = _context.Products
            .Include(p => p.Brand)
            .Include(p => p.ProductCategory)
                .ThenInclude(pc => pc.Category)
            .Where(p => p.ProductCategory.Any(pc => pc.CategoryId == categoryId))
            .ToList();

        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Code = p.Code,
            Name = p.Name,
            KDV = p.KDV,
            BasePrice = p.BasePrice,
            Stock = p.Stock,
            State = p.State,
            Description = p.Description,
            BrandName = p.Brand?.Name,
            CategoryNames = p.ProductCategory?
                                .Select(pc => pc.Category?.Name ?? "")
                                .Where(n => !string.IsNullOrEmpty(n))
                                .ToList()
        }).ToList();

        return productDtos;
    }
    
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
        return p?.Stock;
    }

    public async Task<Product?> UpdateStock(int productId, int newStock)
    {
        if (newStock < 0)
            throw new Exception("New Stock can't be negative");

        var p = await _enumRepository.GetById(productId);
        if (p != null)
        {
            p.Stock = newStock;
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
            p.Stock += amountToIncrease;
            await _enumRepository.Update(p);
            return p;
        }
        return null;
    }

    public async Task<Product?> DecreaseStock(int productId, int amountToDecrease)
    {
        if (amountToDecrease < 0)
            throw new Exception("Amount to decrease can't be negative");

        var p = await _enumRepository.GetById(productId);
        if (p != null)
        {
            p.Stock -= amountToDecrease;
            await _enumRepository.Update(p);
            return p;
        }
        return null;
    } 

}
