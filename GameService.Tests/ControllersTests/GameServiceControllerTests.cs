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
        public async Task GetOwnedGamesAsync_ValidSteamId_ReturnsOkObjectResultWithGames()
        {
            // Arrange
            var steamId = "123456789123456789";
            var games = new List<GameDto> { new GameDto(123, "Test Game", "test.jpg", 123456, false) };
            _mockMediator.Setup(m => m.Send(new GetOwnedGamesQuery(steamId)))
                         .ReturnsAsync(new GetOwnedGamesOutput(games));

            // Act
            var result = await _controller.GetOwnedGamesAsync(steamId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GameDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal(123, returnValue[0].AppId);
        }

        [Fact]
        public async Task GetOwnedGamesAsync_InvalidSteamId_ReturnsBadRequest()
        {
            // Arrange
            var steamId = "invalid";
            _mockMediator.Setup(m => m.Send(new GetOwnedGamesQuery(steamId)))
                         .ThrowsAsync(new ArgumentException("Invalid Steam ID"));

            // Act
            var result = await _controller.GetOwnedGamesAsync(steamId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Steam ID", badRequestResult.Value);
        }

        [Fact]
        public async Task GetGameDetailsAsync_ValidAppId_ReturnsOkObjectResultWithGameDetails()
        {
            // Arrange
            var appId = 123;
            var gameDetails = new GameSchemaDto { GameName = "Test Game", AvailableGameStats = null, GameVersion = "1.1" };
            _mockMediator.Setup(m => m.Send(new GetGameDetailsQuery(appId)))
                         .ReturnsAsync(new GetGameDetailsOutput(gameDetails));

            // Act
            var result = await _controller.GetGameDetailsAsync(appId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GameSchemaDto>(okResult.Value);
        }

        //[Fact]
        //public async Task GetGameDetailsAsync_InvalidAppId_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var appId = -1;         

        //    // Act & Assert
        //    Assert.Throws<ArgumentException>(() => new GetGameDetailsQuery(appId));
        //}
    }
}

