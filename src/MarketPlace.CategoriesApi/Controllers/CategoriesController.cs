using System.Text.RegularExpressions;
using MarketPlace.CategoriesApi.Entities;
using MarketPlace.CategoriesApi.Managers;
using MarketPlace.CategoriesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace MarketPlace.CategoriesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMongoCollection<Category> _categories;
    private readonly IMemoryCache _memoryCache;
    private const string _cacheKey = "categories";

    public CategoriesController(IMongoCollection<Category> categories, IMemoryCache memoryCache)
    {
        var client = new MongoClient("mongodb://root:password@categories_db:270170");
        var database = client.GetDatabase("categories_db");
        _categories = categories;
        _memoryCache = memoryCache;
    }


    private string GenerateKey(string name)
    {
        return Regex.Replace(name.ToLower(), @"\s+", "-");
    }

    [HttpGet]
    public async Task<List<CategoryModel>> GetCategories()
    {
        if (_memoryCache.TryGetValue(_cacheKey, out object? value))
        {
            return (List<CategoryModel>)value!;
        }

        return await GetUpdateCategories();
    }

    private async Task<List<CategoryModel>> GetUpdateCategories()
    {
        var categories = await _categories.Find(_ => true).ToListAsync();
        var categoryModel = CategoryConverter.ConvertToCategoryModels(categories);
        _memoryCache.Set(_cacheKey, categoryModel);
        return categoryModel;
    }

    [HttpGet("soft")]
    public async Task<List<Category>> GetCategoryEntities()
    {
        var categories = await _categories.Find(_ => true).ToListAsync();
        return categories;
    }

    public async Task AddCategory([FromBody] CreateCategoryModel model)
    {
        if (model.ParentId is not null)
        {
            var parentCategory =
                await (await _categories.FindAsync(c => c.ParentId == model.ParentId)).FirstOrDefaultAsync();
            if (parentCategory == null)
            {
                throw new Exception("Parent category is not exists");
            }
        }

        var key = GenerateKey(model.Name);
        var category = new Category()
        {
            Name = model.Name,
            Key = key,
            ParentId = model.ParentId
        };
        await _categories.InsertOneAsync(category);
        await GetUpdateCategories();
    }

}