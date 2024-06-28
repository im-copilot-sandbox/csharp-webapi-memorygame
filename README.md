# Memory Game Web API

This project is a web-based memory game API built with .NET 8.0, utilizing ASP.NET Core. It provides endpoints for game operations such as starting a new game, tracking player progress, and maintaining a leaderboard.

## Features

- **Game Management**: Create and manage game sessions.
- **Leaderboard**: Track high scores across sessions.
- **Player Progress**: Save and retrieve player progress.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or later (optional)

### Running the Project

1. Clone the repository:
   ```sh
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```sh
   cd app
   ```
3. Restore the project dependencies:
   ```sh
   dotnet restore
   ```
4. Build the project:
   ```sh
   dotnet build
   ```
5. Run the project:
   ```sh
   dotnet run
   ```

## Development

### Project Structure

- `Controllers/`: Contains API controllers.
- `Models/`: Contains data models.
- `Services/`: Contains business logic services.
- `data/`: Contains game data in JSON format.

### Adding New Features

1. Create a new branch for your feature:
   ```sh
   git checkout -b feature/<feature-name>
   ```
2. Implement your feature.
3. Write unit tests for your feature in the `app.Tests/` directory.
4. Commit your changes and push to the repository:
   ```sh
   git commit -am "Add <feature-name>"
   git push origin feature/<feature-name>
   ```
5. Open a pull request for your branch.

## Testing

Run the unit tests using the following command:

```sh
dotnet test
```

## Contributing

Contributions are welcome! Please read our [Contributing Guide](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
