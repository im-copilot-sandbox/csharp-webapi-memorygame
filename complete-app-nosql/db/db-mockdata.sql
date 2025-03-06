USE MemoryGame;

-- Insert mock data into Game table
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO Game (Handle, TurnsTaken, TimeTaken)
    VALUES (CONCAT('Player', @i), ABS(CHECKSUM(NEWID()) % 100), ABS(CHECKSUM(NEWID()) % 3600));
    SET @i = @i + 1;
END

-- Insert mock data into Card table
DECLARE @j INT = 1;
WHILE @j <= 100
BEGIN
    INSERT INTO Card (Type, Flipped)
    VALUES (CONCAT('Type', @j), CAST(ROUND(RAND(), 0) AS BIT));
    SET @j = @j + 1;
END

-- Insert mock data into GameCard table
DECLARE @k INT = 1;
WHILE @k <= 100
BEGIN
    INSERT INTO GameCard (GameId, CardId)
    VALUES (ABS(CHECKSUM(NEWID()) % 20) + 1, @k);
    SET @k = @k + 1;
END

-- Insert mock data into Leaderboard table
DECLARE @l INT = 1;
WHILE @l <= 80
BEGIN
    INSERT INTO Leaderboard (Handle, Score, LastPlayed)
    VALUES (CONCAT('Player', @l), ABS(CHECKSUM(NEWID()) % 1000), GETDATE() - ABS(CHECKSUM(NEWID()) % 365));
    SET @l = @l + 1;
END