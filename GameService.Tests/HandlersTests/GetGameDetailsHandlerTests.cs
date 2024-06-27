using GameService.Application.Handlers;
using GameService.Application.Queries;
using GameService.Infrastructure.SteamApi;
using GameService.Application.DTOs;
using GameService.Infrastructure.SteamApi.Models;
using AvailableGameStats = GameService.Infrastructure.SteamApi.Models.AvailableGameStats;
using GameSchemaAchievement = GameService.Infrastructure.SteamApi.Models.GameSchemaAchievement;

namespace GameService.Tests.HandlersTests
{
    public class GetGameDetailsHandlerTests
    {
        private readonly Mock<ISteamApiClient> _mockSteamApiClient;
        private readonly GetGameDetailsHandler _getGameDetailsHandler;

        public GetGameDetailsHandlerTests()
        {
            _mockSteamApiClient = new Mock<ISteamApiClient>();
            _getGameDetailsHandler = new GetGameDetailsHandler(_mockSteamApiClient.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsGameDetails()
        {
            // Arrange
            var appId = 123;
            var query = new GetGameDetailsQuery(appId);
            var gameDetails = new GameSchemaResponse
            {
                GameName = "Test Game",
                GameVersion = "1.0",
                AvailableGameStats = new AvailableGameStats
                {
                    Achievements = new List<GameSchemaAchievement>
                    {
                        new GameSchemaAchievement
                        {
                            DisplayName = "First Achievement",
                            Description = "This is a test achievement",
                            Icon = "http://example.com/icon.png",
                            IconGray = "http://example.com/icon_gray.png",
                            Name = "TEST_ACHIEVEMENT"
                        }
                    }
                }
            };

            _mockSteamApiClient.Setup(client => client.GetAppDetails(appId))
                .ReturnsAsync(gameDetails);

            // Act
            var result = await _getGameDetailsHandler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameDetails.GameName, result.GameDetails.GameName);
            Assert.Equal(gameDetails.GameVersion, result.GameDetails.GameVersion);
            Assert.Single(result.GameDetails.AvailableGameStats.Achievements);
        }

        [Fact]
        public async Task Handle_GameHasNoAchievements_ReturnsGameDetailsWithoutAchievements()
        {
            // Arrange
            var appId = 456;
            var query = new GetGameDetailsQuery(appId);
            var gameDetails = new GameSchemaResponse
            {
                GameName = "Test Game No Achievements",
                GameVersion = "1.0",
                AvailableGameStats = new AvailableGameStats() // No achievements
            };

            _mockSteamApiClient.Setup(client => client.GetAppDetails(appId))
                .ReturnsAsync(gameDetails);

            // Act
            var result = await _getGameDetailsHandler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameDetails.GameName, result.GameDetails.GameName);
            Assert.Equal(gameDetails.GameVersion, result.GameDetails.GameVersion);
            Assert.Empty(result.GameDetails.AvailableGameStats.Achievements);
        }

        [Fact]
        public async Task Handle_InvalidAppId_ReturnsNull()
        {
            // Arrange
            var appId = 123456789;
            var query = new GetGameDetailsQuery(appId);

            _mockSteamApiClient.Setup(client => client.GetAppDetails(appId))
                .ReturnsAsync((GameSchemaResponse)null);

            // Act
            var result = await _getGameDetailsHandler.Handle(query);

            // Assert
            Assert.Null(result);
        }
    }
}