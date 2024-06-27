using GameService.Application.Handlers;
using GameService.Application.Queries;
using GameService.Infrastructure.SteamApi;
using GameService.Infrastructure.SteamApi.Models;


namespace GameService.Tests.HandlersTests
{
    public class GetOwnedGamesHandlerTests
    {
        private readonly Mock<ISteamApiClient> _mockSteamApiClient;
        private readonly GetOwnedGamesHandler _handler;

        public GetOwnedGamesHandlerTests()
        {
            _mockSteamApiClient = new Mock<ISteamApiClient>();
            _handler = new GetOwnedGamesHandler(_mockSteamApiClient.Object);
        }

        [Fact]
        public async Task Handle_ValidSteamId_ReturnsOwnedGames()
        {
            // Arrange
            var steamId = "123456789123456789";
            var games = new List<Game>
        {
            new Game { Appid = 1, Name = "Game 1", PlaytimeForever = 100 },
            new Game { Appid = 1, Name = "Game 2", PlaytimeForever = 200 }
        };
            _mockSteamApiClient.Setup(client => client.GetOwnedGames(steamId))
                .ReturnsAsync(games);

            // Act
            var result = await _handler.Handle(new GetOwnedGamesQuery(steamId));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(games.Count, result.Games.Count());
            _mockSteamApiClient.Verify(client => client.GetOwnedGames(steamId), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidSteamId_ReturnsEmpty()
        {
            // Arrange
            var steamId = "999999999999999999";
            _mockSteamApiClient.Setup(client => client.GetOwnedGames(steamId))
                .ReturnsAsync(new List<Game>());

            // Act
            var result = await _handler.Handle(new GetOwnedGamesQuery(steamId));

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Games);
            _mockSteamApiClient.Verify(client => client.GetOwnedGames(steamId), Times.Once);
        }

        [Fact]
        public async Task Handle_ApiCallFails_ReturnsNull()
        {
            // Arrange
            var steamId = "123456789123456789";
            _mockSteamApiClient.Setup(client => client.GetOwnedGames(steamId))
                .ReturnsAsync((List<Game>)null);

            // Act
            var result = await _handler.Handle(new GetOwnedGamesQuery(steamId));

            // Assert
            Assert.Null(result);
        }
    }
}
