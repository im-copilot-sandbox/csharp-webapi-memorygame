using app.Models;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

namespace app.Data
{
    public class SeedData
    {
        private readonly MongoDbContext _context;

        public SeedData(MongoDbContext context)
        {
            _context = context;
        }
        private async Task SomeMethodAsync()
        {
            // Select all data from the leaderboard ordered by score in descending order
            var leaderboard1 = _context.Leaderboards.Find(_ => true)
                .SortByDescending(entry => entry.Score)
                .ToList();

            
            // Select all data from the leaderboard ordered by last played in ascending order
            var leaderboard2 = _context.Leaderboards.Find(_ => true)
                .SortBy(entry => entry.LastPlayed)
                .ToList();

            // Select all data from the leaderboard ordered by score in descending order and last played in ascending order
            var leaderboard3 = _context.Leaderboards.Find(_ => true)
                .SortByDescending(entry => entry.Score)
                .ThenBy(entry => entry.LastPlayed)
                .ToList();

            // Select all data from the leaderboard where the score is greater than 500
            var leaderboard4 = _context.Leaderboards.Find(entry => entry.Score > 500)
                .ToList();

            // Select all data from the leaderboard where the score is greater than 500 and the last played date is greater than or equal to 2020-01-01
            var leaderboard5 = _context.Leaderboards.Find(entry => entry.Score > 500 && entry.LastPlayed >= new DateTime(2020, 1, 1))
                .ToList();

            // Select the top 10 data from the leaderboard ordered by score in descending order

            var topLeaderboard = _context.Leaderboards.Find(_ => true)
                .SortByDescending(entry => entry.Score)
                .Limit(10)
                .ToList();

            // Select the top 5 scores from the leaderboard that were played after a specific date
            var topLeaderboardAfterDate = _context.Leaderboards.Find(entry => entry.LastPlayed >= new DateTime(2020, 1, 1))
                .SortByDescending(entry => entry.Score)
                .Limit(5)
                .ToList();
            
            // Select the top 5 scores from the leaderboard that were played before a specific date
            var topLeaderboardBeforeDate = _context.Leaderboards.Find(entry => entry.LastPlayed < new DateTime(2020, 1, 1))
                .SortByDescending(entry => entry.Score)
                .Limit(5)
                .ToList();

            // Select the top 5 scores from the leaderboard that were played after a specific date and have a score greater than a specific value
            var topLeaderboardAfterDateAndScore = _context.Leaderboards.Find(entry => entry.LastPlayed >= new DateTime(2020, 1, 1) && entry.Score > 500)
                .SortByDescending(entry => entry.Score)
                .Limit(5)
                .ToList();

            // Select the top 5 scores from the leaderboard that were played before a specific date and have a score greater than a specific value
            var topLeaderboardBeforeDateAndScore = _context.Leaderboards.Find(entry => entry.LastPlayed < new DateTime(2020, 1, 1) && entry.Score > 500)
                .SortByDescending(entry => entry.Score)
                .Limit(5)
                .ToList();

            //Select the top 5 scores from the leaderboard that were played after a specific date and have a score greater than a specific value, ordered by last played date in ascending order
            var topLeaderboardAfterDateAndScoreOrdered = _context.Leaderboards.Find(entry => entry.LastPlayed >= new DateTime(2020, 1, 1) && entry.Score > 500)
                .SortBy(entry => entry.LastPlayed)
                .Limit(5)
                .ToList();
            
            // insert a new score into the leaderboard without specifying the last played date
            var newLeaderboardEntry = new Leaderboard
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Handle = "NewPlayer",
                Score = 750
            };
            _context.Leaderboards.InsertOne(newLeaderboardEntry);

            // insert a new score into the leaderboard with a specific last played date
            var newLeaderboardEntryWithDate = new Leaderboard
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Handle = "PlayerWithDate",
                Score = 800,
                LastPlayed = DateTime.UtcNow // specifying last played date
            };
            _context.Leaderboards.InsertOne(newLeaderboardEntryWithDate);

            // update the score of an existing entry in the leaderboard
            var existingEntry = _context.Leaderboards.Find(entry => entry.Handle == "PlayerWithDate").FirstOrDefault();
            if (existingEntry != null)
            {
                var updatedEntryWithScore = existingEntry with { Score = 900 };
                _context.Leaderboards.ReplaceOne(entry => entry.Id == existingEntry.Id, updatedEntryWithScore);
            }
            
            // update the last played date of an existing entry in the leaderboard
            var existingEntry2 = _context.Leaderboards.Find(entry => entry.Handle == "PlayerWithDate").FirstOrDefault();
            if (existingEntry2 != null)
            {
                var updatedEntryWithDate = existingEntry2 with { LastPlayed = DateTime.UtcNow };
                _context.Leaderboards.ReplaceOne(entry => entry.Id == existingEntry2.Id, updatedEntryWithDate);
            }

            // delete an entry from the leaderboard
            var entryToDelete = _context.Leaderboards.Find(entry => entry.Handle == "PlayerWithDate").FirstOrDefault();
            if (entryToDelete != null)
            {
                _context.Leaderboards.DeleteOne(entry => entry.Id == entryToDelete.Id);
            }

            // delete all entries from the leaderboard
            _context.Leaderboards.DeleteMany(_ => true);

            // delete all entries from the leaderboard where the score is less than 500
            _context.Leaderboards.DeleteMany(entry => entry.Score < 500);

            // delete all entries from the leaderboard where the last played date is before a specific date
            _context.Leaderboards.DeleteMany(entry => entry.LastPlayed < new DateTime(2020, 1, 1));

            // select the average score from the leaderboard
            var averageScore = _context.Leaderboards.Aggregate()
                .Group(new BsonDocument { { "_id", BsonNull.Value }, { "averageScore", new BsonDocument("$avg", "$Score") } })
                .FirstOrDefault();


            Console.WriteLine($"Average Score: {averageScore?["averageScore"]}");


            // select the average score from the leaderboard for entries with a score greater than 500    
            var averageScoreGreaterThan500 = _context.Leaderboards.Aggregate()
                .Match(entry => entry.Score > 500)
                .Group(new BsonDocument { { "_id", BsonNull.Value }, { "averageScore", new BsonDocument("$avg", "$Score") } })
                .FirstOrDefault();

            Console.WriteLine($"Average Score for Scores Greater Than 500: {averageScoreGreaterThan500?["averageScore"]}");

            // select all cards


            var allCards = await _context.Cards.Find(_ => true).ToListAsync();
            foreach (var card in allCards)
            {
                Console.WriteLine($"Card Id: {card.Id}, Type: {card.Type}, Flipped: {card.Flipped}");
            }

            // select all games
            var allGames = await _context.Games.Find(_ => true).ToListAsync();
            foreach (var game in allGames)
            {
                Console.WriteLine($"Game Id: {game.Id}, Handle: {game.Handle}, Turns Taken: {game.TurnsTaken}, Time Taken: {game.TimeTaken}");
            }

            // select all cards that are flipped
            var flippedCards = await _context.Cards.Find(card => card.Flipped).ToListAsync();
            foreach (var card in flippedCards)
            {
                Console.WriteLine($"Flipped Card Id: {card.Id}, Type: {card.Type}");
            }

            // select all cards for a specific game
            var gameId = "some-game-id"; // replace with the actual game id
            var cardsForGame = await _context.Cards.Find(card => card.Id == gameId).ToListAsync();  
            foreach (var card in cardsForGame)
            {
                Console.WriteLine($"Card for Game Id: {card.Id}, Type: {card.Type}, Flipped: {card.Flipped}");
            }   

            
            // select all games for a specific player
            var playerHandle = "some-player-handle"; // replace with the actual player handle
            var gamesForPlayer = await _context.Games.Find(game => game.Handle == playerHandle).ToListAsync();
            foreach (var game in gamesForPlayer)
            {
                Console.WriteLine($"Game for Player Handle: {game.Id}, Turns Taken: {game.TurnsTaken}");
            }

            // include all data from the Cards and order by type in ascending order
            var cardsOrderedByType = await _context.Cards.Find(_ => true)
                .SortBy(card => card.Type)
                .ToListAsync();
            foreach (var card in cardsOrderedByType)
            {
                Console.WriteLine($"Card Id: {card.Id}, Type: {card.Type}, Flipped: {card.Flipped}");
            }

            // select all games with more than 10 turns taken
            var gamesWithMoreThan10Turns = await _context.Games.Find(game => game.TurnsTaken > 10).ToListAsync();
            foreach (var game in gamesWithMoreThan10Turns)
            {
                Console.WriteLine($"Game Id: {game.Id}, Turns Taken: {game.TurnsTaken}");
            }

            // create an index on the games for the field Handle
            await _context.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(
                Builders<Game>.IndexKeys.Ascending(game => game.Handle)));

            // create a compound index on the games for the fields Handle and TurnsTaken
            await _context.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(
                Builders<Game>.IndexKeys.Ascending(game => game.Handle).Ascending(game => game.TurnsTaken)));

            // create a unique index on the games for the field Handle
            await _context.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(
                Builders<Game>.IndexKeys.Ascending(game => game.Handle),
                new CreateIndexOptions { Unique = true }));

            // create a text index on the games for the field Handle
            await _context.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(
                Builders<Game>.IndexKeys.Text(game => game.Handle)));

            // create a hashed index on the games for the field Handle
            
            await _context.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(
                Builders<Game>.IndexKeys.Hashed(game => game.Handle)));

            // create a sparse index on the games for the field Handle
            await _context.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(
                Builders<Game>.IndexKeys.Ascending(game => game.Handle),
                new CreateIndexOptions { Sparse = true }));
            
            // create an index on the cards for the field Id
            await _context.Cards.Indexes.CreateOneAsync(new CreateIndexModel<Card>(
                Builders<Card>.IndexKeys.Ascending(card => card.Id)));

            // create a unique index on the cards for the field Id
            await _context.Cards.Indexes.CreateOneAsync(new CreateIndexModel<Card>(
                Builders<Card>.IndexKeys.Ascending(card => card.Id),
                new CreateIndexOptions { Unique = true }));

            // create a compound index on the cards for the fields Id and Flipped
            await _context.Cards.Indexes.CreateOneAsync(new CreateIndexModel<Card>(
                Builders<Card>.IndexKeys.Ascending(card => card.Id).Ascending(card => card.Flipped)));
            
            // create an index for cards  field Type in ascending order
            await _context.Cards.Indexes.CreateOneAsync(new CreateIndexModel<Card>(
                Builders<Card>.IndexKeys.Ascending(card => card.Type)));

            // create an index on the leaderboard for Handle and order by LastPlayed in descending order
            await _context.Leaderboards.Indexes.CreateOneAsync(new CreateIndexModel<Leaderboard>(
                Builders<Leaderboard>.IndexKeys.Ascending(leaderboard => leaderboard.Handle).Descending(leaderboard => leaderboard.LastPlayed)));

            // create an index on the leaderboard for Score in descending order
            await _context.Leaderboards.Indexes.CreateOneAsync(new CreateIndexModel<Leaderboard>(
                Builders<Leaderboard>.IndexKeys.Descending(leaderboard => leaderboard.Score)));

            // create an index on the leaderboard for LastPlayed
            await _context.Leaderboards.Indexes.CreateOneAsync(new CreateIndexModel<Leaderboard>(
                Builders<Leaderboard>.IndexKeys.Ascending(leaderboard => leaderboard.LastPlayed)));

            // create an index for leaderboard for LastPlayed and order by Score in descending order
            await _context.Leaderboards.Indexes.CreateOneAsync(new CreateIndexModel<Leaderboard>(
                Builders<Leaderboard>.IndexKeys.Ascending(leaderboard => leaderboard.LastPlayed).Descending(leaderboard => leaderboard.Score)));

            // create an index for leaderboard for  LastPlayed and order by Handle in ascending order
            await _context.Leaderboards.Indexes.CreateOneAsync(new CreateIndexModel<Leaderboard>(
                Builders<Leaderboard>.IndexKeys.Ascending(leaderboard => leaderboard.LastPlayed).Ascending(leaderboard => leaderboard.Handle)));
            
            
        }

        public async Task SeedAsync()
        {
            var random = new Random();

            // Seed Cards
            var cards = new List<Card>();
            for (int i = 0; i < 200; i++)
            {
                cards.Add(new Card
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Type = $"Type{i % 10}",
                    Flipped = random.Next(0, 2) == 1
                });
            }
            await _context.Cards.InsertManyAsync(cards);

            // Seed Games
            var games = new List<Game>();
            for (int i = 0; i < 200; i++)
            {
                games.Add(new Game
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Handle = $"Player{i}",
                    TurnsTaken = random.Next(1, 100),
                    TimeTaken = random.Next(1, 1000),
                    Cards = cards.Take(10).ToList()
                });
            }
            await _context.Games.InsertManyAsync(games);

            // Seed Leaderboards
            var leaderboards = new List<Leaderboard>();
            for (int i = 0; i < 200; i++)
            {
                leaderboards.Add(new Leaderboard
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Handle = $"Player{i}",
                    Score = random.Next(1, 1000),
                    LastPlayed = DateTime.UtcNow.AddDays(-random.Next(0, 365))
                });
            }
            await _context.Leaderboards.InsertManyAsync(leaderboards);
        }
    }

    
}