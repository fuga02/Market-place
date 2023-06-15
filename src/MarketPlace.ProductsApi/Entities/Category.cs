namespace MarketPlace.ProductsApi.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<Category>? ChildCategories { get; set; }
    }
}
