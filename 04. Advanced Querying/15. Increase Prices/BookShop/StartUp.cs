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
            IncreasePrices(db);
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var booksBefore2010 = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in booksBefore2010)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }
    }
}


