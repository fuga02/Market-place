﻿namespace MarketPlace.ProductsApi.Models;

public class ProductImageModel
{
    public Guid ProductId { get; set; }
    public IFormFile Image;
}