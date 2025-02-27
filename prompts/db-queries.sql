-- Select all data from the leaderboard table ordered by score in descending order
-- SELECT * FROM Leaderboard ORDER BY Score DESC;

-- Select all data from the leaderboard table ordered by last played in ascending order
SELECT * FROM Leaderboard ORDER BY LastPlayed ASC;

-- Select all data from the leaderboard table ordered by score in descending order and last played in ascending order
SELECT * FROM Leaderboard ORDER BY Score DESC, LastPlayed ASC;

-- Select the top 5 scores from the leaderboard table
SELECT * FROM Leaderboard ORDER BY Score DESC LIMIT 5;

-- Select the top 5 scores from the leaderboard table that were played after a specific date
SELECT * FROM Leaderboard WHERE LastPlayed > '2024-06-17T18:34:15.480172' ORDER BY Score DESC LIMIT 5;

-- Select the top 5 scores from the leaderboard table that were played after a specific date and have a score greater than a specific value
SELECT * FROM Leaderboard WHERE LastPlayed > '2024-06-17T18:34:15.480172' AND Score > 200 ORDER BY Score DESC LIMIT 5;

-- create views related to the 5 queries above
CREATE VIEW TopScores AS
SELECT * FROM Leaderboard ORDER BY Score DESC LIMIT 5;

CREATE VIEW RecentScores AS
SELECT * FROM Leaderboard ORDER BY LastPlayed ASC;

CREATE VIEW TopScoresAndRecent AS
SELECT * FROM Leaderboard ORDER BY Score DESC, LastPlayed ASC;

-- create a trigger that updates the last played date when a new score is inserted
DELIMITER //
CREATE TRIGGER UpdateLastPlayed
BEFORE INSERT ON Leaderboard
FOR EACH ROW
BEGIN
    SET NEW.LastPlayed = NOW();
END //
DELIMITER ;

-- insert a new score into the leaderboard table
INSERT INTO Leaderboard (Handle, Score) VALUES ('newplayer', 500);

-- select all data from the leaderboard table to verify the trigger worked
SELECT * FROM Leaderboard;

-- create a stored procedure for the insert above
DELIMITER //
CREATE PROCEDURE InsertScore(IN newHandle VARCHAR(255), IN newScore INT)
BEGIN
    INSERT INTO Leaderboard (Handle, Score) VALUES (newHandle, newScore);
END //
DELIMITER ;

-- make the procedure above return the id of the inserted row
DELIMITER //
CREATE PROCEDURE InsertScore(IN newHandle VARCHAR(255), IN newScore INT, OUT newId INT)
BEGIN
    INSERT INTO Leaderboard (Handle, Score) VALUES (newHandle, newScore);
    SET newId = LAST_INSERT_ID();
END //
DELIMITER ;

-- call the procedure above and store the result in a variable
SET @newId = 0;
CALL InsertScore('newplayer2', 600, @newId);
SELECT @newId;


-- select the average score from the leaderboard table
SELECT AVG(Score) FROM Leaderboard;

-- create a function select the average score from the leaderboard table
-- that doesn't work in comments but works on chat - no function can be created in comments


-- select all cards that are flipped
SELECT * FROM Cards WHERE Flipped = TRUE;

-- select all cards
SELECT * FROM Cards;


-- select all cards that are flipped
SELECT * FROM Cards WHERE Flipped = TRUE;

-- select all games
SELECT * FROM Games;

-- select all cards for a specific game, 
-- include all data from the Cards table and order bu type in ascending order
SELECT * FROM GameCards
JOIN Cards ON GameCards.CardId = Cards.Id
WHERE GameId = 1
ORDER BY Type ASC;

-- select all games with more than 10 turns taken
SELECT * FROM Games WHERE TurnsTaken > 10;

-- create an idex on the games table for the column Handle
CREATE INDEX HandleIndex ON Games (Handle);

-- create an index on the cards table for the column Type  
CREATE INDEX TypeIndex ON Cards (Type);

-- create an index on the gamecards table for the column GameId
CREATE INDEX GameIdIndex ON GameCards (GameId);

-- create an index on the leaderboard table for the column Handle and order by LastPlayed in descending order
CREATE INDEX HandleLastPlayedIndex ON Leaderboard (Handle, LastPlayed DESC);

-- create a trigger that updates the last played date when a new score is inserted
DELIMITER //
CREATE TRIGGER UpdateLastPlayed
BEFORE INSERT ON Leaderboard
FOR EACH ROW
BEGIN
    SET NEW.LastPlayed = NOW();
END //
DELIMITER ; 

-- insert a new score into the leaderboard table
INSERT INTO Leaderboard (Handle, Score) VALUES ('newplayer', 500);
