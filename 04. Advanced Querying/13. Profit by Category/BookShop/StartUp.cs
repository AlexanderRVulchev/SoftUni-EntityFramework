namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            Console.WriteLine(GetTotalProfitByCategory(db));
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            string[] totalProfitByCategory = context
                .Categories                
                .OrderByDescending(c => c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price))
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price):f2}")
                .ToArray();

            return string.Join(Environment.NewLine, totalProfitByCategory);
        }
    }
}


