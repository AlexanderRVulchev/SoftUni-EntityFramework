For the following tasks, use the BookShop database. You can download the complete project or create it, but you should still use the pre-defined Seed() method from the project to have the same sample data.</br>
### 1.	Book Shop Database
You must create a database for a book shop system. It should look like this:
 
 ![image](https://user-images.githubusercontent.com/106471266/221401964-0aa7d144-5529-4317-8112-fa6bab6af083.png)
 
Constraints</br>
Your namespaces should be:</br>
•	BookShop – for your StartUp class</br>
•	BookShop.Data – for your DbContext</br>
•	BookShop.Models – for your models </br>
•	BookShop.Models.Enums – for your models</br>
Your models should be:</br>
•	BookShopContext – your DbContext</br>
•	Author</br>
o	AuthorId</br>
o	FirstName (up to 50 characters, unicode, not required)</br>
o	LastName (up to 50 characters, unicode)</br>
•	Book</br>
o	BookId</br>
o	Title (up to 50 characters, unicode)</br>
o	Description (up to 1000 characters, unicode)</br>
o	ReleaseDate (not required)</br>
o	Copies (an integer)</br>
o	Price</br>
o	EditionType – enum (Normal, Promo, Gold)</br>
o	AgeRestriction – enum (Minor, Teen, Adult)</br>
o	Author</br>
o	BookCategories</br>
•	Category</br>
o	CategoryId</br>
o	Name (up to 50 characters, unicode)</br>
o	CategoryBooks</br>
•	BookCategory – mapping entity</br>
For the following tasks, you will be creating methods that accept a BookShopContext as a parameter and use it to run some queries. Create those methods inside your StartUp class and upload your whole solution to Judge.</br>
### 2.	Age Restriction
NOTE: You will need method public static string GetBooksByAgeRestriction(BookShopContext context, string command) and public StartUp class. </br>
Return in a single string all book titles, each on a new line, that have an age restriction, equal to the given command. Order the titles alphabetically.</br>
Read input from the console in your main method and call your method with the necessary arguments. Print the returned string to the console. Ignore the casing of the input.</br>
##### Example</br>
Input	Output</br>
miNor	A Confederacy of Dunces</br>
A Farewell to Arms</br>
A Handful of Dust</br>
…</br>
teEN	A Passage to India</br>
A Scanner Darkly</br>
A Swiftly Tilting Planet</br>
…</br>
### 3.	Golden Books
NOTE: You will need method public static string GetGoldenBooks(BookShopContext context) and public StartUp class. </br>
Return in a single string the titles of the golden edition books that have less than 5000 copies, each on a new line. Order them by BookId ascending.</br>
Call the GetGoldenBooks(BookShopContext context) method in your Main() and print the returned string to the console.</br>
##### Example</br>
Output</br>
Behold the Man</br>
Bury My Heart at Wounded Knee</br>
The Cricket on the Hearth</br>
…</br>
### 4.	Books by Price
NOTE: You will need method public static string GetBooksByPrice(BookShopContext context) and public StartUp class. </br>
Return in a single string all titles and prices of books with a price higher than 40, each on a new row in the format given below. Order them by price descending.</br>
##### Example</br>
Output</br>
O Pioneers! - $49.90</br>
That Hideous Strength - $48.63</br>
A Handful of Dust - $48.63</br>
…</br>
### 5.	Not Released In
NOTE: You will need method public static string GetBooksNotReleasedIn(BookShopContext context, int year) and public StartUp class. </br>
Return in a single string with all titles of books that are NOT released in a given year. Order them by bookId ascending.</br>
##### Example</br>
Input	Output</br>
2000	Absalom</br>
After Many a Summer Dies the Swan</br>
Ah</br>
…</br>
1998	Ah</br>
Wilderness!</br>
Alien CornÂ (play)</br>
…</br>
### 6.	Book Titles by Category
NOTE: You will need method public static string GetBooksByCategory(BookShopContext context, string input) and public StartUp class. </br>
Return in a single string the titles of books by a given list of categories. The list of categories will be given in a single line separated by one or more spaces. Ignore casing. Order by title alphabetically.</br>
##### Example</br>
Input	Output</br>
horror mystery drama	A Fanatic Heart</br>
A Farewell to Arms</br>
A Glass of Blessings</br>
…</br>
### 7.	Released Before Date
NOTE: You will need method public static string GetBooksReleasedBefore(BookShopContext context, string date) and public StartUp class. </br>
Return the title, edition type and price of all books that are released before a given date. The date will be a string in the format "dd-MM-yyyy".</br>
Return all of the rows in a single string, ordered by release date (descending).</br>
##### Example</br>
Input	Output</br>
12-04-1992	If I Forget Thee Jerusalem - Gold - $33.21</br>
Oh! To be in England - Normal - $46.67</br>
The Monkey's Raincoat - Normal - $46.93</br>
…</br>
30-12-1989	A Fanatic Heart - Normal - $9.41</br>
The Curious Incident of the Dog in the Night-Time - Normal - $23.41</br>
The Other Side of Silence - Gold - $46.26</br>
…</br>
### 8.	Author Search
NOTE: You will need method public static string GetAuthorNamesEndingIn(BookShopContext context, string input) and public StartUp class. </br>
Return the full names of authors, whose first name ends with a given string.</br>
Return all names in a single string, each on a new row, ordered alphabetically.</br>
##### Example</br>
Input	Output</br>
e	George Powell</br>
Jane Ortiz</br>
dy	Randy Morales</br>
### 9.	Book Search
NOTE: You will need method public static string GetBookTitlesContaining(BookShopContext context, string input) and public StartUp class. </br>
Return the titles of the book, which contain a given string. Ignore casing.</br>
Return all titles in a single string, each on a new row, ordered alphabetically.</br>
##### Example</br>
Input	Output</br>
sK	A Catskill Eagle</br>
The Daffodil Sky</br>
The Skull Beneath the Skin</br>
WOR	Great Work of Time</br>
Terrible Swift Sword</br>
### 10.	Book Search by Author
NOTE: You will need method public static string GetBooksByAuthor(BookShopContext context, string input) and public StartUp class. </br>
Return all titles of books and their authors' names for books, which are written by authors whose last names start with the given string.</br>
Return a single string with each title on a new row. Ignore casing. Order by BookId ascending.</br>
##### Example</br>
Input	Output</br>
R	A Handful of Dust (Bozhidara Rysinova)</br>
Have His Carcase (Bozhidara Rysinova)</br>
The Heart Is a Lonely Hunter (Bozhidara Rysinova) </br>
…</br>
po	Postern of Fate (Stanko Popov)</br>
Precious Bane (Stanko Popov)</br>
The Proper Study (Stanko Popov)</br>
…</br>
### 11.	Count Books
NOTE: You will need method public static int CountBooks(BookShopContext context, int lengthCheck) and public StartUp class. </br>
Return the number of books, which have a title longer than the number given as an input.</br>
##### Example</br>
Input	Output	Comments</br>
12	169	There are 169 books with longer title than 12 symbols</br>
40	2	There are 2 books with longer title than 40 symbols</br>
### 12.	Total Book Copies
NOTE: You will need method public static string CountCopiesByAuthor(BookShopContext context) and public StartUp class. </br>
Return the total number of book copies for each author. Order the results descending by total book copies.</br>
Return all results in a single string, each on a new line.</br>
##### Example</br>
Output</br>
Stanko Popov - 117778</br>
Lyubov Ivanova - 107391</br>
Jane Ortiz – 103673</br>
…</br>
### 13.	Profit by Category
NOTE: You will need method public static string GetTotalProfitByCategory(BookShopContext context) and public StartUp class. </br>
Return the total profit of all books by category. Profit for a book can be calculated by multiplying its number of copies by the price per single book. Order the results by descending by total profit for a category and ascending by category name. Print the total profit formatted to the second digit.</br>
##### Example</br>
Output</br>
Art $6428917.79</br>
Fantasy $5291439.71</br>
Adventure $5153920.77</br>
Children's $4809746.22</br>
…</br>
### 14.	Most Recent Books
NOTE: You will need method public static string GetMostRecentBooks(BookShopContext context) and public StartUp class.</br>
Get the most recent books by categories. The categories should be ordered by name alphabetically. Only take the top 3 most recent books from each category – ordered by release date (descending). Select and print the category name and for each book – its title and release year.</br>
##### Example</br>
Output</br>
--Action</br>
Brandy ofthe Damned (2015)</br>
Bonjour Tristesse (2013)</br>
By Grand Central Station I Sat Down and Wept (2010)</br>
--Adventure</br>
The Cricket on the Hearth (2013)</br>
Dance Dance Dance (2002)</br>
Cover Her Face (2000)</br>
…</br>
### 15.	Increase Prices
NOTE: You will need method public static void IncreasePrices(BookShopContext context) and public StartUp class.</br>
Increase the prices of all books released before 2010 by 5.</br>
### 16.	Remove Books
NOTE: You will need method public static int RemoveBooks(BookShopContext context) and public StartUp class.</br>
Remove all books, which have less than 4200 copies. Return an int - the number of books that were deleted from the database.</br>
##### Example</br>
Output</br>
34</br>

