using MarketPlace.ProductsApi.Entities;
using MarketPlace.ProductsApi.Models;

namespace MarketPlace.ProductsApi.Repositories;

public interface IProductRepository
{
    Task AddProduct(Category category, Product product);
    Task UpdateProduct(Category category, Product product);
    Task DeleteProduct(Category category, Product product);
    Task<Product> GetProductById(Category category, Guid productId );
    Task<List<Product>> GetProducts(Category category);
}

public class ProductRepository : IProductRepository
{
    public Task AddProduct(Category category, Product product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProduct(Category category, Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(Category category, Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductById(Category category, Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetProducts(Category category)
    {
        throw new NotImplementedException();
    }
}