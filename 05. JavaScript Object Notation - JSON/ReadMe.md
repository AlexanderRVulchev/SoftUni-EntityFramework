# Products Shop Database
A products shop holds users, products and categories for the products. Users can sell and buy products.</br>
•	Users have an Id, FirstName (optional), LastName and Age (optional).</br>
•	Products have an Id, Name, Price, BuyerId (optional) and SellerId as Ids of Users.</br>
•	Categories have an Id and Name.</br>
•	Using Entity Framework and Code First create a database, following the above description.</br>

![image](https://user-images.githubusercontent.com/106471266/222803475-27f6f979-2a43-4ca5-914b-d2ca680534af.png)
 
•	Users should have many Products sold and many Products bought. </br>
•	Products should have many Categories.</br>
•	Categories should have many Products.</br>
•	CategoriesProducts should map Products and Categories.</br>
## 1.	Import Data
### Query 1. Import Users
NOTE: You will need method public static string ImportUsers(ProductShopContext context, string inputJson) and public StartUp class. </br>
Import the users from the provided file "users.json".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {users.Count}";</br>
### Query 2. Import Products
NOTE: You will need method public static string ImportProducts(ProductShopContext context, string inputJson) and public StartUp class. </br>
Import the users from the provided file "products.json".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {products.Count}";</br>
### Query 3. Import Categories
NOTE: You will need method public static string ImportCategories(ProductShopContext context, string inputJson) and public StartUp class. </br>
Import the users from the provided file "categories.json". Some of the names will be null, so you don't have to add them to the database. Just skip the record and continue.</br>
Your method should return a string with the following message:</br>
 $"Successfully imported {categories.Count}";</br>
### Query 4. Import Categories and Products
NOTE: You will need method public static string ImportCategoryProducts(ProductShopContext context, string inputJson) and public StartUp class. </br>
Import the users from the provided file "categories-products.json". </br>
Your method should return a string with the message:</br>
$"Successfully imported {categoryProducts.Count}";</br>
## 2.	Export Data
Write the below-described queries and export the returned data to the specified format. Make sure that Entity Framework Core generates only a single query for each task.</br>
Note that because of the random generation of the data, the output probably will be different.</br>
### Query 5. Export Products in Range
NOTE: You will need method public static string GetProductsInRange(ProductShopContext context) and public StartUp class. </br>
Get all products in a specified price range:  500 to 1000 (inclusive). Order them by price (from lowest to highest). Select only the product name, price and the full name of the seller. Export the result to JSON.</br>

### Query 6. Export Sold Products
NOTE: You will need method public static string GetSoldProducts(ProductShopContext context) and public StartUp class. </br>
Get all users who have at least 1 sold item with a buyer. Order them by the last name, then by the first name. Select the person's first and last name. For each of the sold products (products with buyers), select the product's name, price and the buyer's first and last name.</br>

### Query 7. Export Categories by Products Count
NOTE: You will need method public static string GetCategoriesByProductsCount(ProductShopContext context) and public StartUp class. </br>
Get all categories. Order them in descending order by the category's products count. For each category select its name, the number of products, the average price of those products (rounded to the second digit after the decimal separator) and the total revenue (total price sum and rounded to the second digit after the decimal separator) of those products (regardless if they have a buyer or not).</br>

### Query 8. Export Users and Products
NOTE: You will need method public static string GetUsersWithProducts(ProductShopContext context) and public StartUp class. </br>
Get all users who have at least 1 sold product with a buyer. Order them in descending order by the number of sold products to a buyer. Select only their last name and age and for each product – name and price. Ignore all null values.</br>
Export the results to JSON. Follow the format below to better understand how to structure your data. </br>

# Car Dealer
## 1.	Setup Database
A car dealer needs information about cars, their parts, parts suppliers, customers and sales. </br>
•	Cars have Make, Model, TraveledDistance in kilometers.</br>
•	Parts have Name, Price and Quantity.</br>
•	Supplier has a Name and info whether they supply imported parts.</br>
•	Customer has a Name, BirthDate and info whether they are a young driver (young driver is a driver that has less than 2 years of experience. Those customers get an additional 5% off for the sale.).</br>
•	Sale has a Car, Customer and a Discount percentage.</br>
A Price of a Car is formed by the total price of its Parts.</br>
 
 ![image](https://user-images.githubusercontent.com/106471266/222803624-57473f83-6b51-4342-a5e3-d76bc7a99743.png)
 
•	A Car has many Parts and one Part can be placed in many Cars.</br>
•	One Supplier can supply many Parts and each Part can be delivered by only one Supplier.</br>
•	In one Sale, only one Car can be sold to only one Customer.</br>
•	A Customer can buy many Cars.</br>
## 2.	Import Data
Import data from the provided files ("suppliers.json", "parts.json", "cars.json", "customers.json") </br>
### Query 9. Import Suppliers
NOTE: You will need method public static string ImportSuppliers(CarDealerContext context, string inputJson) and public StartUp class. </br>
Import the suppliers from the provided file "suppliers.json". </br>
Your method should return a string with the following message:</br>
$"Successfully imported {suppliers.Count}.";</br>
### Query 10. Import Parts
NOTE: You will need method public static string ImportParts(CarDealerContext context, string inputJson) and public StartUp class. </br>
Import the parts from the provided file "parts.json". If the supplierId doesn't exist in the Suppliers table, skip the record.</br>
Your method should return a string with the following message:</br>
$"Successfully imported {parts.Count}.";</br>
### Query 11. Import Cars
NOTE: You will need method public static string ImportCars(CarDealerContext context, string inputJson) and public StartUp class. </br>
Import the cars from the provided file "cars.json".</br>
Your method should return string with the following message:</br>
$"Successfully imported {cars.Count}.";</br>
### Query 12. Import Customers
NOTE: You will need method public static string ImportCustomers(CarDealerContext context, string inputJson) and public StartUp class. </br>
Import the customers from the provided file "customers.json".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {customers.Count}.";</br>
### Query 13. Import Sales
NOTE: You will need method public static string ImportSales(CarDealerContext context, string inputJson) and public StartUp class. </br>
Import the sales from the provided file "sales.json".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {sales.Count}.";</br>
## 3.	Export Data
Write the below described queries and export the returned data to the specified format. Make sure that Entity Framework Core generates only a single query for each task.</br>
### Query 14. Export Ordered Customers
NOTE: You will need method public static string GetOrderedCustomers(CarDealerContext context) and public StartUp class. </br>
Get all customers ordered by their birth date ascending. If two customers are born on the same date first print those who are not young drivers (e.g., print experienced drivers first). Export the list of customers to JSON in the format provided below.</br>

### Query 15. Export Cars from Make Toyota
NOTE: You will need method public static string GetCarsFromMakeToyota(CarDealerContext context) and public StartUp class. </br>
Get all cars with Toyota make and order them by model alphabetically and by traveled distance descending. Export the list of cars to JSON in the format provided below.</br>

### Query 16. Export Local Suppliers
NOTE: You will need method public static string GetLocalSuppliers(CarDealerContext context) and public StartUp class. </br>
Get all suppliers that do not import parts from abroad. Get their id, name and the number of parts they can offer to supply. Export the list of suppliers to JSON in the format provided below.</br>

### Query 17. Export Cars with Their List of Parts
NOTE: You will need method public static string GetCarsWithTheirListOfParts(CarDealerContext context) and public StartUp class. </br>
Get all cars along with their list of parts. For the car get only make, model and traveled distance and for the parts get only name and price (formatted to 2nd digit after the decimal point). Export the list of cars and their parts to JSON in the format provided below.</br>

### Query 18. Export Total Sales by Customer
NOTE: You will need method public static string GetTotalSalesByCustomer(CarDealerContext context) and public StartUp class. </br>
Get all customers that have bought at least 1 car and get their names, bought cars count and total spent money on cars. Order the result list by total spent money descending, then by total bought cars again in descending order. Export the list of customers to JSON in the format provided below.</br>

### Query 19. Export Sales with Applied Discount
NOTE: You will need method public static string GetSalesWithAppliedDiscount(CarDealerContext context) and public StartUp class. </br>
Get first 10 sales with information about the car, customer and price of the sale with and without discount. Export the list of sales to JSON in the format provided below.</br>
