namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            string command = Console.ReadLine();
            string output =  GetBooksByAgeRestriction(db, command);
            Console.WriteLine(output);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var enumValue = Enum.Parse<AgeRestriction>(command, true);

            string[] bookTitles = context
                .Books
                .Where(b => b.AgeRestriction == enumValue)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            return String.Join(Environment.NewLine, bookTitles);
        }
    }


}


