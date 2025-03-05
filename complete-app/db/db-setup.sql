USE MemoryGame;

CREATE TABLE Game (
    GameId INT PRIMARY KEY IDENTITY(1,1),
    Handle NVARCHAR(100) NOT NULL,
    TurnsTaken INT NOT NULL,
    TimeTaken INT NOT NULL
);

CREATE TABLE Card (
    CardId INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50) NOT NULL,
    Flipped BIT NOT NULL DEFAULT 0
);

CREATE TABLE GameCard (
    GameId INT NOT NULL,
    CardId INT NOT NULL,
    PRIMARY KEY (GameId, CardId),
    FOREIGN KEY (GameId) REFERENCES Game(GameId),
    FOREIGN KEY (CardId) REFERENCES Card(CardId)
);

CREATE TABLE Leaderboard (
    LeaderboardId INT PRIMARY KEY IDENTITY(1,1),
    Handle NVARCHAR(100) NOT NULL,
    Score INT NOT NULL,
    LastPlayed DATETIME NOT NULL
);