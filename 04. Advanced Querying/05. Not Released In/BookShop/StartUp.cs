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

            int year = int.Parse(Console.ReadLine());
            Console.WriteLine(GetBooksNotReleasedIn(db, year));
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            string[] booksNotReleasedIn = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, booksNotReleasedIn);
        }
    }
}


