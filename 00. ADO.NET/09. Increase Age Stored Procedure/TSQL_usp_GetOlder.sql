 CREATE OR ALTER PROC usp_GetOlder(@Id INT)
 AS 
 UPDATE Minions
 SET Age += 1
 WHERE Id = @Id