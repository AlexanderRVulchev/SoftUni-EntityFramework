## 1.	Hospital Database</br>
You went to your GP for your annual exam and you told him that you’ve started work as a Junior Database App Developer. It turned out he was looking for someone to make an app, which he could use to manage and store data about his patients.</br>
Your task is to design a database using the Code First approach. The GP needs to keep information about his patients. Each patient has first name, last name, address, email, information whether he has medical insurance or not and should keep history about all his visitations, diagnoses and prescribed medicaments. Each visitation has date and comments. Each diagnose has name and comments for it. Each medicament has name. Validate all data before inserting it in the database.</br>
Your Database should look something like this:</br>
Remember! With Entity Framework Core you can have different column names from your classes’ property names!</br>

 ![image](https://user-images.githubusercontent.com/106471266/224808345-41621a48-8227-4ef0-953a-4452c668d38d.png)

Constraints</br>
Your namespaces should be:</br>
•	P01_HospitalDatabase – for your Startup class, if you have one</br>
•	P01_HospitalDatabase.Data – for your DbContext</br>
•	P01_HospitalDatabase.Data.Models – for your models</br>
Note: Do not use separated projects, because Judge will return Compile Time Error.</br>
Your classes should be:</br>
•	HospitalContext – your DbContext</br>
•	Patient:</br>
	PatientId</br>
	FirstName (up to 50 characters, unicode)</br>
	LastName (up to 50 characters, unicode)</br>
	Address (up to 250 characters, unicode)</br>
	Email (up to 80 characters, not unicode)</br>
	HasInsurance</br>
•	Visitation:</br>
	VisitationId</br>
	Date</br>
	Comments (up to 250 characters, unicode)</br>
	Patient</br>
•	Diagnose:</br>
	DiagnoseId</br>
	Name (up to 50 characters, unicode)</br>
	Comments (up to 250 characters, unicode)</br>
	Patient</br>
•	Medicament:</br>
	MedicamentId</br>
	Name (up to 50 characters, unicode)</br>
•	PatientMedicament – mapping class between Patients and Medicaments</br>
The collections of mapping classes (ICollection<PatientMedicament>) must be named Prescriptions!</br>
Note: Don’t forget to remove the Tools package before uploading your project to Judge, if you have used it!</br>
Don’t use version of Entity Framework Core above 3.1.3!</br>
Bonus Task</br>
Make a console-based user interface, so the doctor can easily use the database.</br>
## 2.	Hospital Database Modification
Your GP bragged around in the hospital about the cool software you made for him. Now the hospital administration wants to modify your program so they can use it too. They want to store information about the doctors (name and specialty). Each doctor can perform many visitations. Make the necessary changes in the database to satisfy the new needs of the hospital administration. </br>
Constraints</br>
Keep the namespaces from the previous task and only add the class Doctor and change the class Visitation accordingly. The doctor’s name and specialty should be up to 100 characters long, unicode.</br>

## 3.	Sales Database
Create a database for storing data about sales using the Code First approach. The database should look like this:</br>
 
![image](https://user-images.githubusercontent.com/106471266/224808462-85479580-2f90-4270-a4b6-5d3ca44d94dd.png)
  
Constraints</br>
Your namespaces should be:</br>
•	P03_SalesDatabase</br>
•	P03_SalesDatabase.Data</br>
•	P03_SalesDatabase.Data.Models</br>
Your classes should be:</br>
•	SalesContext – your DbContext</br>
•	Product:</br>
	ProductId</br>
	Name (up to 50 characters, unicode)</br>
	Quantity (real number)</br>
	Price</br>
	Sales</br>
•	Customer:</br>
	CustomerId</br>
	Name (up to 100 characters, unicode)</br>
	Email (up to 80 characters, not unicode)</br>
	CreditCardNumber (string)</br>
	Sales</br>
•	Store:</br>
	StoreId</br>
	Name (up to 80 characters, unicode)</br>
	Sales</br>
•	Sale:</br>
	SaleId</br>
	Date</br>
	Product</br>
	Customer</br>
	Store</br>
