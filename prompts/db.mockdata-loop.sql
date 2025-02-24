-- Insert mock data into Cards table
DELIMITER //
CREATE PROCEDURE InsertCards()
BEGIN
    DECLARE i INT DEFAULT 1;
    WHILE i <= 200 DO
        INSERT INTO Cards (Type, Flipped)
        VALUES 
        (CONCAT('type', i), IF(i % 2 = 0, TRUE, FALSE));
        SET i = i + 1;
    END WHILE;
END //
DELIMITER ;
CALL InsertCards();
DROP PROCEDURE InsertCards;

-- Insert mock data into Games table
DELIMITER //
CREATE PROCEDURE InsertGames()
BEGIN
    DECLARE i INT DEFAULT 1;
    WHILE i <= 200 DO
        INSERT INTO Games (Handle, TurnsTaken, TimeTaken)
        VALUES 
        (CONCAT('player', i), FLOOR(RAND() * 30) + 1, FLOOR(RAND() * 1000) + 100);
        SET i = i + 1;
    END WHILE;
END //
DELIMITER ;
CALL InsertGames();
DROP PROCEDURE InsertGames;

-- Insert mock data into GameCards table
DELIMITER //
CREATE PROCEDURE InsertGameCards()
BEGIN
    DECLARE i INT DEFAULT 1;
    DECLARE j INT DEFAULT 1;
    WHILE i <= 200 DO
        WHILE j <= 10 DO
            INSERT INTO GameCards (GameId, CardId)
            VALUES 
            (i, j);
            SET j = j + 1;
        END WHILE;
        SET j = 1;
        SET i = i + 1;
    END WHILE;
END //
DELIMITER ;
CALL InsertGameCards();
DROP PROCEDURE InsertGameCards;

-- Insert mock data into Leaderboard table
DELIMITER //
CREATE PROCEDURE InsertLeaderboard()
BEGIN
    DECLARE i INT DEFAULT 1;
    WHILE i <= 200 DO
        INSERT INTO Leaderboard (Handle, Score, LastPlayed)
        VALUES 
        (CONCAT('player', i), FLOOR(RAND() * 1000) + 100, DATE_ADD('2024-01-01', INTERVAL FLOOR(RAND() * 365) DAY));
        SET i = i + 1;
    END WHILE;
END //
DELIMITER ;
CALL InsertLeaderboard();
DROP PROCEDURE InsertLeaderboard;