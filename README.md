# C# Web API Memory Game

This project is a C# Web API for a memory game. It includes functionality for saving and retrieving game data, as well as managing a leaderboard.

## Project Structure

The project is organized into the following main directories:

- `app/Models`: Contains the data models for the game and leaderboard.
- `app/Services`: Contains the service classes for handling game data and leaderboard operations.
- `app/Controllers`: Contains the API controllers for handling HTTP requests.
- `app.Tests`: Contains the unit tests for the project.

## Models

### Game

The `Game` model represents the state of a game. It includes the following properties:

- `Handle`: The unique identifier for the game.
- `TurnsTaken`: The number of turns taken in the game.
- `TimeTaken`: The time taken to complete the game.
- `Cards`: A list of `Card` objects representing the cards in the game.

### Card

The `Card` model represents a card in the game. It includes the following properties:

- `Type`: The type of the card.
- `Flipped`: A boolean indicating whether the card is flipped.

### Leaderboard

The `Leaderboard` model represents an entry in the leaderboard. It includes the following properties:

- `Handle`: The unique identifier for the player.
- `Score`: The score of the player.
- `LastPlayed`: The date and time when the player last played.

## Services

### GameData

The `GameData` service provides methods for saving and retrieving game data and leaderboard entries. The methods include:

- `SaveGameAsync(Game game, string handle)`: Saves the game data to a JSON file.
- `RetrieveGameAsync(string handle)`: Retrieves the game data from a JSON file.
- `SaveLeaderboardEntryAsync(string handle, int score)`: Saves a leaderboard entry to a JSON file.
- `RetrieveLeaderboardAsync()`: Retrieves the leaderboard data from a JSON file.

## Controllers

### RoutesController

The `RoutesController` handles HTTP requests for the game and leaderboard. The endpoints include:

- `GET /greeting`: Returns a greeting message.
- `POST /game`: Saves the game data.
- `GET /game/{handle}`: Retrieves the game data for a specific handle.
- `POST /leaderboard`: Saves a leaderboard entry.
- `GET /leaderboard`: Retrieves the leaderboard data.

## Unit Tests

The unit tests are located in the `app.Tests` directory. The tests include:

- `GameDataTests`: Tests for the `GameData` service methods.

## Running the Project

To run the project, follow these steps:

1. Clone the repository.
2. Open the project in Visual Studio or your preferred C# IDE.
3. Build the project to restore the dependencies.
4. Run the project.

## Running the Tests

To run the unit tests, follow these steps:

1. Open the project in Visual Studio or your preferred C# IDE.
2. Build the project to restore the dependencies.
3. Run the tests using the test runner in your IDE.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more information.