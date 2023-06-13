namespace MarketPlace.Product.Blazor.Models;

public class ProductImage
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public string Image_Url { get; set; }
}