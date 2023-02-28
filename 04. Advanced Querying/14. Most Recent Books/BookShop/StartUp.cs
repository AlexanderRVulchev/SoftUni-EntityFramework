namespace BookShop
{
    using Data;
    using Initializer;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            Console.WriteLine(GetMostRecentBooks(db));
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var mostRecentBooks = context
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Books = c.CategoryBooks
                        .OrderByDescending(cb => cb.Book.ReleaseDate)
                        .Select(cb => new
                        {
                            BookTitle = cb.Book.Title,
                            BookYear = cb.Book.ReleaseDate.Value.Year
                        })
                        .Take(3)                                            
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var c in mostRecentBooks)
            {
                sb.AppendLine($"--{c.CategoryName}");
                foreach (var b in c.Books)
                {
                    sb.AppendLine($"{b.BookTitle} ({b.BookYear})");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}


