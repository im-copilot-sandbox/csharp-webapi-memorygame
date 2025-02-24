# Memory Game API

This is a Memory Game API built with ASP.NET Core. The API allows players to save and retrieve game data, as well as manage a leaderboard.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Running the Tests](#running-the-tests)
- [Generating Code Coverage Report](#generating-code-coverage-report)
- [API Endpoints](#api-endpoints)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features

- Save and retrieve game data
- Manage a leaderboard
- Input validation
- Unit tests with xUnit
- Code coverage with coverlet and ReportGenerator

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Installation

1. Clone the repository:
   git clone https://github.com/yourusername/memory-game-api.git cd memory-game-api

2. Restore the dependencies:
   dotnet restore


3. Build the project:
   dotnet build


## Running the Tests

To run the unit tests, use the following command:
dotnet test

## Generating Code Coverage Report

To generate a code coverage report, follow these steps:

1. Run tests with code coverage:
   dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./TestResults/

2. Generate the code coverage report:
   reportgenerator -reports:./TestResults/coverage.opencover.xml -targetdir:./coverage-report

3. Open the report:
   Open the `index.html` file in the `./coverage-report` directory in your browser.

## API Endpoints

### Greeting

- **GET /api/greeting**
  - Returns a greeting message.

### Game

- **POST /api/game**
  - Saves game data.
  - Request body: `Game` object.

- **GET /api/game/{handle}**
  - Retrieves game data for a specific player handle.

### Leaderboard

- **POST /api/leaderboard**
  - Saves a leaderboard entry.
  - Request body: `Leaderboard` object.

- **GET /api/leaderboard**
  - Retrieves the top 10 players in the leaderboard.

## Project Structure

memory-game-api/ ├── app/ │   ├── Controllers/ │   │   └── RoutesController.cs │   ├── Models/ │   │   ├── Game.cs │   │   ├── Leaderboard.cs │   │   └── Card.cs │   ├── Services/ │   │   └── GameData.cs │   └── app.csproj ├── app.Tests/ │   ├── RoutesControllerTests.cs │   └── app.Tests.csproj ├── README.md └── .gitignore


## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Explanation:
•	Features: Lists the main features of the project.
•	Getting Started: Provides instructions for setting up the project.
•	Running the Tests: Explains how to run the unit tests.
•	Generating Code Coverage Report: Provides steps to generate a code coverage report.
•	API Endpoints: Describes the available API endpoints.
•	Project Structure: Shows the structure of the project.
•	Contributing: Encourages contributions and provides guidelines.
•	License: Specifies the license for the project.
This README.md file should provide a comprehensive overview of your project and help others get started with it.
