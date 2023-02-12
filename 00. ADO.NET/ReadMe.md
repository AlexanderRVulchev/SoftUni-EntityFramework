### 1. Initial Setup 
Write a program that connects to your localhost server. Create a new database called MinionsDB where we will keep information about our minions and villains. </br>
For each minion, we should keep information about its name, age and town. </br>
Each town has information about the country where it is located. Villains have name and evilness factors (super good, good, bad, evil, super evil). Each minion can serve several villains and each villain can have several minions serving him. Fill all tables with at least 5 records each.</br>
In the end, you should have the following tables: </br>
•	Countries</br>
•	Towns</br>
•	Minions</br>
•	EvilnessFactors</br>
•	Villains</br>
•	MinionsVillains</br>
 
### 2. Villain Names
Write a program that prints on the console all villains' names and their number of minions of those who have more than 3 minions ordered descending by the number of minions.</br>
###### Example
Output</br>
Gru - 6</br>
Victor - 4</br>
Jilly – 4</br>
### 3. Minion Names
Write a program that prints on the console all minion names and ages for a given villain id, ordered by name alphabetically.</br>
If there is no villain with the given ID, print "No villain with ID <VillainId> exists in the database.".</br>
If the selected villain has no minions, print "(no minions)" on the second row.</br>
###### Example
Input	Output</br>
###### 1	
Villain: Gru</br>
1. Bob 13</br>
2. Kevin 14</br>
3. Steward 19		</br>
###### 3	
Villain: Victor</br>
1. Bob 13</br>
2. Simon 22		</br>
###### 2	
Villain: Victor Jr.</br>
(no minions)</br>
###### 10	
No villain with ID 10 exists in the database.</br>
### 4. Add Minion
Write a program that reads information about a minion and its villain and adds it to the database. In case the town of the minion is not in the database, insert it as well. In case the villain is not present in the database, add him too with a default evilness factor of "evil". Finally set the new minion to be a servant of the villain. Print appropriate messages after each operation.</br>
###### Input
The input comes in two lines:</br>
•	On the first line, you will receive the minion information in the format "Minion: <Name> <Age> <TownName>"</br>
•	On the second – the villain information in the format "Villain: <Name>"</br>
###### Output
After completing an operation, you must print one of the following messages:</br>
•	After adding a new town to the database: "Town <TownName> was added to the database."</br>
•	After adding a new villain to the database: "Villain <VillainName> was added to the database."</br>
•	Finally, after successfully adding the minion to the database and making it a servant of a villain: "Successfully added <MinionName> to be minion of <VillainName>."</br>
*Bonus task: Make sure all operations are executed successfully. In case of an error do not change the database.</br>
###### Example
Input	Output</br>
###### Minion: Bob 14 Berlin Villain: Gru	</br>
Successfully added Robert to be minion of Gru.</br>
###### Minion: Cathleen 20 Liverpool Villain: Gru	
Town Liverpool was added to the database.</br>
Successfully added Cathleen to be minion of Gru.</br>
###### Minion: Mars 23 Sofia Villain: Poppy	</br>
Villain Poppy was added to the database.</br>
Successfully added Mars to be minion of Poppy.</br>
###### Minion: Carry 20 Eindhoven Villain: Jimmy	</br>
Town Eindhoven was added to the database.</br>
Villain Jimmy was added to the database.</br>
Successfully added Carry to be minion of Jimmy.</br>
### 5. Change Town Names Casing
Write a program that changes all town names to uppercase for a given country. </br>
You will receive one line of input with the name of the country.</br>
Print the number of towns that were changed in the format "<ChangedTownsCount> town names were affected.". On a second line, print the names that were changed, separated by a comma and a space.</br>
If no towns were affected (the country does not exist in the database or has no cities connected to it), print "No town names were affected.".</br>
Example</br>
Input	Output</br>
###### Bulgaria	
3 town names were affected. </br>
[SOFIA, VARNA, BURGAS]
###### Germany
No town names were affected.</br>
### 6. Remove Villain 
Write a program that receives the ID of a villain, deletes him from the database and releases his minions from serving to him. Print on two lines the name of the deleted villain in the format "<Name> was deleted." and the number of minions released in format "<MinionCount> minions were released.". Make sure all operations go as planned, otherwise do not make any changes in the database.</br>
If there is no villain in the database with the given ID, print "No such villain was found.".</br>
Example</br>
Input	Output</br>
###### 1	
Gru was deleted.</br>
6 minions were released.</br>
###### 3	
Victor was deleted.</br>
0 minions were released.</br>
###### 101	
No such villain was found.</br>
### 7. Print All Minion Names
Write a program that prints all minion names from the minions table in the following order: first record, last record, first + 1, last - 1, first + 2, last - 2 … first + n, last - n. </br>
1	10	2	9	3	8	4	7	5	6</br>
Example</br>
Original Order	Output</br>
Bob Kevin Steward Jimmy Vicky Becky Jully	</br>
Bob Jully Kevin Becky Steward Vicky Jimmy</br>
### 8. Increase Minion Age
Read from the console minion IDs separated by space. Increment the age of those minions by 1 and make the title of their name case. Finally, print the name and the age of all minions in the database, each on a new row in the format "<Name> <Age>".</br>
Example</br>
Minions</br>
Id	Name	Age</br>
1	bob	14</br>
2	stuart	22</br>
3	kevin	13</br>
4	jimmy	49</br>
5	vicky jackson	26</br>

Input	Output		</br>
2 1 4	Bob 15</br>
Stuart 23</br>
kevin 13</br>
Jimmy 50</br>
vicky jackson 26		5	bob 14</br>
stuart 22</br>
kevin 13</br>
jimmy 49</br>
Vicky Jackson 27</br>
### 9. Increase Age Stored Procedure 
Create stored procedure usp_GetOlder (directly in the database using Management Studio or any other similar tool) that receives MinionId and increases that minion's age by 1. Write a program that uses that stored procedure to increase the age of a minion whose id will be given as input from the console. After that print the name and the age of that minion.</br>
Example</br>
Minions</br>
Id	Name	Age</br>
1	bob	14</br>
2	steward	22</br>
3	kevin	13</br>
4	jimmy	49</br>
5	vicky jackson	26</br>
</br>
Input	Output</br>
1	bob – 15 years old</br>
3	kevin – 14 years old</br>
5	vicky jackson – 27 years old</br>
