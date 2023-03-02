using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            ImportData(context);

            string jsonResult = GetProductsInRange(context);
            Console.WriteLine(jsonResult);
        }

        public static void ImportData(ProductShopContext context)
        {
            string inputJsonUsers = File.ReadAllText(@"../../../Datasets/users.json");
            string inputJsonProducts = File.ReadAllText(@"../../../Datasets/products.json");
            string inputJsonCategories = File.ReadAllText(@"../../../Datasets/categories.json");
            string inputJsonCategoriesProducts = File.ReadAllText(@"../../../Datasets/categories-products.json");

            List<User>? users = JsonConvert
                .DeserializeObject<List<User>>(inputJsonUsers);
            context.Users.AddRange(users);
            context.SaveChanges();

            List<Product> products = JsonConvert
                .DeserializeObject<List<Product>>(inputJsonProducts);
            context.Products.AddRange(products);
            context.SaveChanges();

            List<Category> categories = JsonConvert
                .DeserializeObject<List<Category>>(inputJsonCategories)
                .Where(c => c.Name != null)
                .ToList();
            context.Categories.AddRange(categories);
            context.SaveChanges();

            List<CategoryProduct> categoryProducts = JsonConvert
               .DeserializeObject<List<CategoryProduct>>(inputJsonCategoriesProducts);
            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName,
                })
                .ToArray();
            
            return JsonConvert.SerializeObject(productsInRange, Formatting.Indented);            
        }
    }
}