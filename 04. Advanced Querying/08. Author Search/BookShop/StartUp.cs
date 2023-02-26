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

            string input = Console.ReadLine();
            Console.WriteLine(GetAuthorNamesEndingIn(db, input));
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            string[] authorNamesEndingIn = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => a.FirstName + " " + a.LastName)
                .OrderBy(name => name)
                .ToArray();

            return string.Join(Environment.NewLine, authorNamesEndingIn);
        }
    }
}


