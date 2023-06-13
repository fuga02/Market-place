using Microsoft.AspNetCore.Http;

namespace MarketPlace.Product.Blazor.Models;

public class ProductImageModel
{
    public Guid ProductId { get; set; }
    public IFormFile Image;
}