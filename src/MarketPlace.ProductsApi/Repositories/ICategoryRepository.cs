using MarketPlace.ProductsApi.Entities;

namespace MarketPlace.ProductsApi.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; set; }
    
     Task AddCategory(Category category);
     Task UpdateCategory(Category category);
     Task DeleteCategory(Category category);
     Task<Category> GetCategoryById(int categoryId);
}