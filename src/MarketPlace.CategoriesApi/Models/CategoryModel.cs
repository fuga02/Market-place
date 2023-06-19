namespace MarketPlace.CategoriesApi.Models;

public class CategoryModel
{
   public Guid Id { get; set; }
   public required string Name { get; set; }
   public required string Key { get; set; }
   public List<CategoryModel>? SubCategories { get; set; }
}