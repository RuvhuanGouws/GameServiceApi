# Game Service API

## Table of Contents

-   [About](#about)
-   [Features](#features)
-   [Getting Started](#getting-started)
    -   [Prerequisites](#prerequisites)
    -   [Installation](#installation)
-   [Usage](#usage)
-   [Endpoints](#endpoints)
-   [Architecture](#architecture)
-   [Contributing](#contributing)

## About

The Game Service API is a RESTful API designed to provide information about Steam users' achievements and games. It integrates with the Steam Web API to fetch relevant data, allowing developers and gamers to create custom tools, websites, and dashboards to track their achievement progress.

## Features

-   Add users with their SteamIDs to the platform
-   Add groups and manage members of groups (coming soon)
-   Fetch a user's Steam game library.
-   Retrieve achievement details for specific games.
-   Get a user's progress on game achievements. (coming soon)

## Getting Started

### Prerequisites

-   A valid Steam Web API key (obtain from [https://steamcommunity.com/dev/apikey](https://steamcommunity.com/dev/apikey))
-   .NET 8 SDK or later
-   An Azure Cosmos DB account (if you want to store user data persistently)

### Installation

1.  Clone the repository: `git clone [https://github.com/YourUsername/SteamAchievementHunterAPI.git](https://github.com/RuvhuanGouws/GameServiceApi)`
2.  Set up your Cosmos DB connection string in `appsettings.json`:
    ```json
    {
      "Cosmos_ConnectionString": "<your-connection-string>"
    }
    ```
3.  Set your Steam API key as an environment variable named `STEAM_API_KEY`.
4.  Build and run the project: `dotnet run`

## Usage

The API is self-documenting using Swagger UI. Once the application is running, navigate to `https://localhost:7193/swagger/index.html` in your browser to explore the available endpoints and try them out.

## Endpoints

-   **GET /api/gameservice/user/{steamId}**: Get a user's owned games.
-   **GET /api/gameservice/{appId}**: Get details of a specific game.
-   **GET /api/user**: Get all users.
-   **GET /api/user/{steamId}**: Get details of a specific user by steam id.
-   **POST /api/user**: Create a new user.
-   (More endpoints to be added)

## Architecture
-   **Domain-Driven Design (DDD):** The domain model is designed to reflect the core concepts and relationships of the problem domain.
-   **Mediator Pattern:** A custom mediator pattern has been implemented to facilitate communication between API and Infrastructure layers.
-   **Dependency Injection:** The application uses dependency injection to manage dependencies and make the code more testable.
-   **Cosmos DB:**  The API uses Azure Cosmos DB as the database for storing user data.
-   **Steam Web API:** The API integrates with the Steam Web API to fetch game and achievement data.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues.
