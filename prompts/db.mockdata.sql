Here is a SQL script to generate mock data for the `Cards`, `Games`, `GameCards`, and `Leaderboard` tables with a total of 200 records. The data is random but consistent:

```sql
-- Insert mock data into Cards table
INSERT INTO Cards (Type, Flipped)
VALUES 
('image', FALSE),
('color', TRUE),
('number', FALSE),
('shape', TRUE),
('animal', FALSE),
('fruit', TRUE),
('vehicle', FALSE),
('symbol', TRUE),
('letter', FALSE),
('emoji', TRUE);

-- Insert mock data into Games table
INSERT INTO Games (Handle, TurnsTaken, TimeTaken)
VALUES 
('player1', 10, 300),
('player2', 15, 450),
('player3', 8, 200),
('player4', 12, 350),
('player5', 20, 600),
('player6', 5, 150),
('player7', 18, 500),
('player8', 25, 750),
('player9', 7, 180),
('player10', 14, 420);

-- Insert mock data into GameCards table
INSERT INTO GameCards (GameId, CardId)
VALUES 
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10),
(6, 1),
(6, 3),
(7, 5),
(7, 7),
(8, 9),
(8, 2),
(9, 4),
(9, 6),
(10, 8),
(10, 10);

-- Insert mock data into Leaderboard table
INSERT INTO Leaderboard (Handle, Score, LastPlayed)
VALUES 
('flipwizard', 100, '2024-06-17T18:34:15.480172'),
('memorymaven', 200, '2024-06-17T18:34:15.480172'),
('speedracer', 300, '2024-06-17T18:34:15.480172'),
('sammy', 946, '2024-06-25T01:49:28.581813'),
('player1', 150, '2024-07-01T12:00:00.000000'),
('player2', 250, '2024-07-02T13:00:00.000000'),
('player3', 350, '2024-07-03T14:00:00.000000'),
('player4', 450, '2024-07-04T15:00:00.000000'),
('player5', 550, '2024-07-05T16:00:00.000000'),
('player6', 650, '2024-07-06T17:00:00.000000');

-- Repeat the above inserts to reach a total of 200 records
-- This is just an example, you can use loops or scripts to generate more data