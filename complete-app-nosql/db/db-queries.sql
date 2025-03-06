


-- Select all data from the leaderboard table ordered by score in descending order
SELECT * FROM Leaderboard ORDER BY Score DESC;

-- Select all data from the leaderboard table ordered by last played in ascending order
SELECT * FROM Leaderboard ORDER BY LastPlayed ASC;

-- Select all data from the leaderboard table ordered by score in descending order and last played in ascending order
SELECT * FROM Leaderboard ORDER BY Score DESC, LastPlayed ASC;

-- Select the top 10 data from the leaderboard table ordered by score in descending order
SELECT TOP 10 * FROM Leaderboard ORDER BY Score DESC;

-- Select the top 5 scores from the leaderboard table that were played after a specific date
SELECT TOP 5 * FROM Leaderboard WHERE LastPlayed > '2021-01-01' ORDER BY Score DESC;

-- Select the top 5 scores from the leaderboard table that were played before a specific date
SELECT TOP 5 * FROM Leaderboard WHERE LastPlayed < '2021-01-01' ORDER BY Score DESC;

-- Select the top 5 scores from the leaderboard table that were played after a specific date and have a score greater than a specific value
SELECT TOP 5 * FROM Leaderboard WHERE LastPlayed > '2021-01-01' AND Score > 500 ORDER BY Score DESC;


-- please recreate those views without the ORDER BY clause, as it is not allowed in views
CREATE VIEW LeaderboardByScore AS
SELECT * FROM Leaderboard;

CREATE VIEW LeaderboardByLastPlayed AS
SELECT * FROM Leaderboard;

CREATE VIEW LeaderboardByScoreAndLastPlayed AS
SELECT * FROM Leaderboard;

CREATE VIEW Top10LeaderboardByScore AS
SELECT TOP 10 * FROM Leaderboard;

CREATE VIEW Top5LeaderboardAfterDate AS
SELECT TOP 5 * FROM Leaderboard WHERE LastPlayed > '2021-01-01';

CREATE VIEW Top5LeaderboardBeforeDate AS
SELECT TOP 5 * FROM Leaderboard WHERE LastPlayed < '2021-01-01';

CREATE VIEW Top5LeaderboardAfterDateAndScore AS
SELECT TOP 5 * FROM Leaderboard WHERE LastPlayed > '2021-01-01' AND Score > 500;







-- create a trigger that updates the last played date when a new score is inserted
CREATE TRIGGER UpdateLastPlayed
ON Leaderboard
AFTER INSERT
AS
BEGIN
    UPDATE Leaderboard
    SET LastPlayed = GETDATE()
    WHERE LeaderboardId IN (SELECT LeaderboardId FROM inserted);
END;

-- create a trigger that updates the last played date when a score is updated
CREATE TRIGGER UpdateLastPlayedOnUpdate
ON Leaderboard
AFTER UPDATE
AS
BEGIN
    UPDATE Leaderboard
    SET LastPlayed = GETDATE()
    WHERE LeaderboardId IN (SELECT LeaderboardId FROM inserted);
END;    



-- insert a new score into the leaderboard table without specifying the last played date
INSERT INTO Leaderboard (Handle, Score)
VALUES ('NewPlayer', 1000);

-- update the score of an existing player in the leaderboard table without specifying the last played date
UPDATE Leaderboard
SET Score = 1500
WHERE Handle = 'NewPlayer';




-- Make LastPlayed accept nulls
ALTER TABLE Leaderboard
ALTER COLUMN LastPlayed DATETIME NULL;


-- create a stored procedure for the insert above
CREATE PROCEDURE InsertScore
    @Handle NVARCHAR(100),
    @Score INT
AS
BEGIN
    INSERT INTO Leaderboard (Handle, Score)
    VALUES (@Handle, @Score);
END;

-- create a stored procedure for the update above
CREATE PROCEDURE UpdateScore
    @Handle NVARCHAR(100),
    @Score INT
AS
BEGIN
    UPDATE Leaderboard
    SET Score = @Score
    WHERE Handle = @Handle;
END;


-- alter the procedure above return the id of the inserted row
ALTER PROCEDURE InsertScore
    @Handle NVARCHAR(100),
    @Score INT
AS
BEGIN
    INSERT INTO Leaderboard (Handle, Score)
    VALUES (@Handle, @Score);

    SELECT SCOPE_IDENTITY() AS LeaderboardId;
END;

-- alter the procedure above return the id of the updated row
ALTER PROCEDURE UpdateScore
    @Handle NVARCHAR(100),
    @Score INT
AS
BEGIN
    UPDATE Leaderboard
    SET Score = @Score
    WHERE Handle = @Handle;

    SELECT LeaderboardId FROM Leaderboard WHERE Handle = @Handle;
END;

-- call the InsertScore procedure above
EXEC InsertScore 'NewPlayer', 1000;

-- select the average score from the leaderboard table
SELECT AVG(Score) AS AverageScore FROM Leaderboard;


-- create a function to select the average score from the leaderboard table
CREATE FUNCTION GetAverageScore()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AverageScore FLOAT;
    SELECT @AverageScore = AVG(Score) FROM Leaderboard;
    RETURN @AverageScore;
END;


-- select all cards that are flipped
SELECT * FROM Card WHERE Flipped = 1;


-- select all cards

SELECT * FROM Card;




-- select all cards for a specific game, 
-- include all data from the Cards table and order by type in ascending order
SELECT Card.*
FROM Card
JOIN GameCard ON Card.CardId = GameCard.CardId
WHERE GameCard.GameId = 1
ORDER BY Type ASC;


-- select all games with more than 10 turns taken
SELECT * FROM Game WHERE TurnsTaken > 10;

-- create an idex on the games table for the column Handle
CREATE INDEX IX_Game_Handle ON Game (Handle);


-- create an index on the cards table for the column Type  
CREATE INDEX IX_Card_Type ON Card (Type);

-- create an index on the gamecards table for the column GameId
CREATE INDEX IX_GameCard_GameId ON GameCard (GameId);

-- create an index on the leaderboard table for the column Handle and order by LastPlayed in descending order
CREATE INDEX IX_Leaderboard_Handle_LastPlayed ON Leaderboard (Handle, LastPlayed DESC);

-- create an index on the leaderboard table for the column Score
CREATE INDEX IX_Leaderboard_Score ON Leaderboard (Score);

-- create an index on the leaderboard table for the column LastPlayed
CREATE INDEX IX_Leaderboard_LastPlayed ON Leaderboard (LastPlayed);

-- create an index on the leaderboard table for the column LastPlayed and order by Score in descending order
CREATE INDEX IX_Leaderboard_LastPlayed_Score ON Leaderboard (LastPlayed, Score DESC);

-- create an index on the leaderboard table for the column LastPlayed and order by Handle in ascending order
CREATE INDEX IX_Leaderboard_LastPlayed_Handle ON Leaderboard (LastPlayed, Handle ASC);



