namespace MarketPlace.ProductsApi.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public List<ProductImage> Images { get; set; }
}

public class ProductImage
{
    public int Id { get; set; }
    public string Image_Url { get; set; }
}