
### 1.	Student System
Your task is to create a database for the StudentSystem, using the EF Core Code First approach. It should look like this:
 
 ![image](https://user-images.githubusercontent.com/106471266/221130791-9270fb72-e3b4-419c-a0a2-b2cc510ab7ec.png)
 
Constraints</br>
Your namespaces should be:</br>
•	P01_StudentSystem – for your Startup class, if you have one</br>
•	P01_StudentSystem.Data – for your DbContext</br>
•	P01_StudentSystem.Data.Models – for your models</br>
Your models should be:</br>
•	StudentSystemContext – your DbContext</br>
•	Student</br>
o	StudentId</br>
o	Name – up to 100 characters, unicode</br>
o	PhoneNumber – exactly 10 characters, not unicode, not required</br>
o	RegisteredOn</br>
o	Birthday – not required</br>
•	Course</br>
o	CourseId</br>
o	Name – up to 80 characters, unicode</br>
o	Description – unicode, not required</br>
o	StartDate</br>
o	EndDate</br>
o	Price</br>
•	Resource</br>
o	ResourceId</br>
o	Name – up to 50 characters, unicode</br>
o	Url – not unicode</br>
o	ResourceType – enum, can be Video, Presentation, Document or Other</br>
o	CourseId</br>
•	Homework</br>
o	HomeworkId</br>
o	Content – string, linking to a file, not unicode</br>
o	ContentType - enum, can be Application, Pdf or Zip</br>
o	SubmissionTime</br>
o	StudentId</br>
o	CourseId</br>
•	StudentCourse – mapping between Students and Courses</br>
Table relations:	</br>
•	One student can have many Courses </br>
•	One student can have many Homeworks </br>
•	One course can have many Students</br>
•	One course can have many Resources</br>
•	One course can have many Homeworks</br>
You will need a constructor, accepting DbContextOptions to test your solution in Judge!</br>

### 2.	Football Betting
Your task is to create a database for a FootballBookmakerSystem, using the Code First approach. It should look like this:</br>
 
 ![image](https://user-images.githubusercontent.com/106471266/221130843-c67f1e5f-3d7d-409e-9a30-419d3d877baf.png)
 
Constraints</br>
Your namespaces should be:</br>
•	P02_FootballBetting – for your Startup class, if you have one</br>
•	P02_FootballBetting.Data – for your DbContext</br>
•	P02_FootballBetting.Data.Models – for your models</br>
Your models should be:</br>
•	FootballBettingContext – your DbContext</br>
•	Team – TeamId, Name, LogoUrl, Initials (JUV, LIV, ARS…), Budget, PrimaryKitColorId, SecondaryKitColorId, TownId</br>
•	Color – ColorId, Name</br>
•	Town – TownId, Name, CountryId</br>
•	Country – CountryId, Name</br>
•	Player – PlayerId, Name, SquadNumber, TeamId, PositionId, IsInjured</br>
•	Position – PositionId, Name</br>
•	PlayerStatistic – GameId, PlayerId, ScoredGoals, Assists, MinutesPlayed</br>
•	Game – GameId, HomeTeamId, AwayTeamId, HomeTeamGoals, AwayTeamGoals, DateTime, HomeTeamBetRate, AwayTeamBetRate, DrawBetRate, Result</br>
•	Bet – BetId, Amount, Prediction, DateTime, UserId, GameId</br>
•	User – UserId, Username, Password, Email, Name, Balance</br>
Table relationships:</br>
•	A Team has one PrimaryKitColor and one SecondaryKitColor</br>
•	A Color has many PrimaryKitTeams and many SecondaryKitTeams</br>
•	A Team residents in one Town</br>
•	A Town can host several Teams</br>
•	A Game has one HomeTeam and one AwayTeam and a Team can have many HomeGames and many AwayGames</br>
•	A Town can be placed in one Country and a Country can have many Towns</br>
•	A Player can play for one Team and one Team can have many Players</br>
•	A Player can play at one Position and one Position can be played by many Players</br>
•	One Player can play in many Games and in each Game, many Players take part (both collections must be named PlayersStatistics)</br>
•	Many Bets can be placed on one Game, but a Bet can be only on one Game</br>
•	Each bet for given game must have Prediction result</br>
•	A Bet can be placed by only one User and one User can place many Bets</br>
Separate the models, data and client into different layers (projects).</br>
