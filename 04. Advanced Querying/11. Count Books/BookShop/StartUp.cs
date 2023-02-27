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

            int inputNum = int.Parse(Console.ReadLine());
            Console.WriteLine(CountBooks(db, inputNum));
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int countOfBooks = context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return countOfBooks;
        }
    }
}


