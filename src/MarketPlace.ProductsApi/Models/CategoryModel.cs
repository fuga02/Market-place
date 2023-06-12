using MarketPlace.ProductsApi.Entities;

namespace MarketPlace.ProductsApi.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentId { get; set; }
    public List<Category>? ChildCategories { get; set; }
    public List<Product> Products { get; set; }
}