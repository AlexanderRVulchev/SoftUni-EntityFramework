# Product Shop Database
## 1.	Setup Database
A products shop holds users, products and categories for the products. Users can sell and buy products.</br>
•	Users have an Id, FirstName (optional), LastName and Age (optional).</br>
•	Products have an Id, Name, Price, BuyerId (optional) and SellerId as Ids of Users.</br>
•	Categories have an Id and Name.</br>
•	Using Entity Framework and Code First create a database, following the above description.</br>

![image](https://user-images.githubusercontent.com/106471266/223167105-51eb3fda-c121-4455-ac47-cd8295ea289b.png)
 
•	Users should have many Products sold and many Products bought. </br>
•	Products should have many Categories.</br>
•	Categories should have many Products.</br>
•	CategoryProducts should map Products and Categories.</br>
## 2.	Import Data
### Query 1. Import Users
NOTE: You will need method public static string ImportUsers(ProductShopContext context, string inputXml) and public StartUp class. </br>
Import the users from the provided file "users.xml".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {users.Count}";</br>
### Query 2. Import Products
NOTE: You will need method public static string ImportProducts(ProductShopContext context, string inputXml) and public StartUp class. </br>
Import the products from the provided file "products.xml".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {products.Count}";</br>
### Query 3. Import Categories
NOTE: You will need method public static string ImportCategories(ProductShopContext context, string inputXml) and public StartUp class. </br>
Import the categories from the provided file "categories.xml". </br>
Some of the names will be null, so you don't have to add them to the database. Just skip the record and continue.</br>
Your method should return a string with the following message:</br>
$"Successfully imported {categories.Count}";</br>
### Query 4. Import Categories and Products
NOTE: You will need method public static string ImportCategoryProducts(ProductShopContext context, string inputXml) and public StartUp class. </br>
Import the categories and products ids from the provided file "categories-products.xml". If provided CategoryId or ProductId doesn't exist, skip the whole entry!</br>
Your method should return a string with the message:</br>
$"Successfully imported {categoryProducts.Count}";</br>
## 3.	Query and Export Data
Write the below-described queries and export the returned data to the specified format. Make sure that Entity Framework Core generates only a single query for each task.</br>
### Query 5. Export Products In Range
NOTE: You will need method public static string GetProductsInRange(ProductShopContext context) and public StartUp class. </br>
Get all products in a specified price range between 500 and 1000 (inclusive). Order them by price (from lowest to highest). Select only the product name, price and the full name of the buyer. Take top 10 records.</br>

### Query 6. Export Sold Products
NOTE: You will need method public static string GetSoldProducts(ProductShopContext context) and public StartUp class. </br>
Get all users who have at least 1 sold item. Order them by the last name, then by the first name. Select the person's first and last name. For each of the sold products, select the product's name and price. Take top 5 records. </br>

### Query 7. Export Categories By Products Count
NOTE: You will need method public static string GetCategoriesByProductsCount(ProductShopContext context) and public StartUp class. </br>
Get all categories. For each category select its name, the number of products, the average price of those products and the total revenue (total price sum) of those products (regardless if they have a buyer or not). Order them by the number of products (descending), then by total revenue.</br>

### Query 8. Export Users and Products
NOTE: You will need method public static string GetUsersWithProducts(ProductShopContext context) and public StartUp class. </br>
Select users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest). Select only their first and last name, age, count of sold products and for each product - name and price sorted by price (descending). Take top 10 records.</br>
Follow the format below to better understand how to structure your data. </br>
Return the list of suppliers to XML in the format provided below.</br>

# Car Dealer
## 1.	Setup Database
A car dealer needs information about cars, their parts, parts suppliers, customers and sales. </br>
•	Cars have Make, Model, TraveledDistance in kilometers.</br>
•	Parts have Name, Price and Quantity.</br>
•	Supplier has a Name and info whether they supply imported parts.</br>
•	Customer has a Name, BirthDate and info whether they are a young driver (young driver is a driver that has less than 2 years of experience. Those customers get an additional 5% off for the sale.).</br>
•	Sale has a Car, Customer and a Discount percentage.</br>
A Price of a Car is formed by the total price of its Parts.</br>
        
 ![image](https://user-images.githubusercontent.com/106471266/223167182-950d7fdc-98eb-4894-b51f-bbf6b079e809.png)

•	A Car has many Parts and one Part can be placed in many Cars.</br>
•	One Supplier can supply many Parts and each Part can be delivered by only one Supplier.</br>
•	In one Sale, only one Car can be sold to only one Customer.</br>
•	A Customer can buy many Cars.</br>
## 2.	Import Data
Import data from the provided files ("suppliers.xml", "parts.xml", "cars.xml", "customers.xml").</br>
### Query 9. Import Suppliers
NOTE: You will need method public static string ImportSuppliers(CarDealerContext context, string inputXml) and public StartUp class. </br>
Import the suppliers from the provided file "suppliers.xml". </br>
Your method should return a string with the following message:</br>
$"Successfully imported {suppliers.Count}";</br>
### Query 10. Import Parts
NOTE: You will need method public static string ImportParts(CarDealerContext context, string inputXml) and public StartUp class. </br>
Import the parts from the provided file "parts.xml". If the supplierId doesn't exist, skip the record.</br>
Your method should return a string with the message:</br>
$"Successfully imported {parts.Count}";</br>
### Query 11. Import Cars
NOTE: You will need method public static string ImportCars(CarDealerContext context, string inputXml) and public StartUp class. </br>
Import the cars from the provided file "cars.xml". Select unique car part ids. If the partId doesn't exist, skip the Part record.</br>
Your method should return a string with the following message:</br>
$"Successfully imported {cars.Count}";</br>
### Query 12. Import Customers
NOTE: You will need method public static string ImportCustomers(CarDealerContext context, string inputXml) and public StartUp class. </br>
Import the customers from the provided file "customers.xml".</br>
Your method should return a string with the following message:</br>
$"Successfully imported {customers.Count}";</br>
### Query 13. Import Sales
NOTE: You will need method public static string ImportSales(CarDealerContext context, string inputXml) and public StartUp class. </br>
Import the sales from the provided file "sales.xml". If car doesn't exist, skip whole entity.</br>
Your method should return a string with the following message:</br>
$"Successfully imported {sales.Count}";</br>
## 3.	Query and Export Data
Write the below-described queries and export the returned data to the specified format. Make sure that Entity Framework generates only a single query for each task.</br>
### Query 14. Export Cars With Distance
NOTE: You will need method public static string GetCarsWithDistance(CarDealerContext context) and public StartUp class. </br>
Get all cars with a distance of more than 2,000,000. Order them by make, then by model alphabetically. Take top 10 records.</br>

### Query 15. Export Cars from Make BMW
NOTE: You will need method public static string GetCarsFromMakeBmw(CarDealerContext context) and public StartUp class. </br>
Get all cars from make BMW and order them by model alphabetically and by traveled distance descending.</br>

### Query 16. Export Local Suppliers
NOTE: You will need method public static string GetLocalSuppliers(CarDealerContext context) and public StartUp class. </br>
Get all suppliers that do not import parts from abroad. Get their id, name and the number of parts they can offer to supply. </br>

### Query 17. Export Cars with Their List of Parts
NOTE: You will need method public static string GetCarsWithTheirListOfParts(CarDealerContext context) and public StartUp class. </br>
Get all cars along with their list of parts. For the car get only make, model and traveled distance and for the parts get only name and price and sort all parts by price (descending). Sort all cars by traveled distance (descending) and then by the model (ascending). Select top 5 records.</br>

### Query 18. Export Total Sales by Customer
NOTE: You will need method public static string GetTotalSalesByCustomer(CarDealerContext context) and public StartUp class. </br>
Get all customers that have bought at least 1 car and get their names, bought cars count and total spent money on cars. Order the result list by total spent money (descending). Don't forget that young drivers get a discount!</br>

### Query 19. Export Sales with Applied Discount
NOTE: You will need method public static string GetSalesWithAppliedDiscount(CarDealerContext context) and public StartUp class. </br>
Get all sales with information about the car, customer and price of the sale with and without discount. Don't take under consideration the young driver discount!</br>
