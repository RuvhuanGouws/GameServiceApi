using GameService.Api.Controllers;
using GameService.Application.Boundaries;
using GameService.Application.DTOs;
using GameService.Application.DTOs.GameSchema;
using GameService.Application.Mediator;
using GameService.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Tests.ControllersTests
{
    public class GameServiceControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly GameServiceController _controller;

        public GameServiceControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new GameServiceController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetOwnedGamesAsync_WithValidSteamId_ReturnsOkResult()
        {
            // Arrange
            var steamId = "123456789123456789";
            var games = new GetOwnedGamesOutput(new List<GameDto>{ new GameDto(123, "Test Game", "test.jpg", 123456, false) });
            _mockMediator.Setup(m => m.Send(It.IsAny<GetOwnedGamesQuery>())).ReturnsAsync(games);

            // Act
            var result = await _controller.GetOwnedGamesAsync(steamId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GameDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal(123, returnValue[0].AppId);
        }

        [Fact]
        public async Task GetOwnedGamesAsync_WithInvalidSteamId_ReturnsBadRequest()
        {
            // Arrange
            var steamId = "invalidsteamid";
            _mockMediator.Setup(m => m.Send(It.IsAny<GetOwnedGamesQuery>()))
                         .ThrowsAsync(new ArgumentException("Invalid Steam ID format."));

            // Act
            var result = await _controller.GetOwnedGamesAsync(steamId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Steam ID format.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetGameDetailsAsync_WithValidAppId_ReturnsOkResult()
        {
            // Arrange
            var appId = 123;
            var gameDetails = new GameSchemaDto { GameName = "Test Game", GameVersion = "1.0" };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetGameDetailsQuery>())).ReturnsAsync(new GetGameDetailsOutput(gameDetails));

            // Act
            var result = await _controller.GetGameDetailsAsync(appId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GameSchemaDto>(okResult.Value);
            Assert.Equal("Test Game", returnValue.GameName);
        }
    }
}

