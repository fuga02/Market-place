namespace MarketPlace.CategoriesApi.Models;

public class CreateCategoryModel
{
    public required string Name { get; set; }
    public Guid? ParentId { get; set; }
}