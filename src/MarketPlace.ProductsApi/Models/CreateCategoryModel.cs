namespace MarketPlace.ProductsApi.Models;

public class CreateCategoryModel
{
    public required string Name { get; set; }
    public int? ParentId { get; set; }
}