using GameService.Api.Controllers;
using GameService.Application.Boundaries;
using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Mediator;
using GameService.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GameService.Tests.ControllersTests
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new UserController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsOkObjectResult_WithListOfUserDtos()
        {
            // Arrange
            var userDtos = new List<UserDto> { new UserDto { SteamId = "123", DisplayName = "Test User" } };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>())).ReturnsAsync(new GetUsersOutput(userDtos));

            // Act
            var result = await _controller.GetUsersAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GetUsersOutput>(okResult.Value);
            Assert.Single(returnValue.Users);
            Assert.Equal("123", returnValue.Users[0].SteamId);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>())).ReturnsAsync(new GetUserOutput((UserDto)null));

            // Act
            var result = await _controller.GetUserByIdAsync(Guid.NewGuid().ToString());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUserBySteamIdAsync_ReturnsBadRequest_WhenArgumentExceptionThrown()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetUserBySteamIdQuery>())).ThrowsAsync(new System.ArgumentException("Invalid Steam ID"));

            // Act
            var result = await _controller.GetUserBySteamIdAsync("invalid");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Steam ID format.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsCreatedAtAction_WhenUserIsSuccessfullyCreated()
        {
            // Arrange
            var createUserCommand = new CreateUserCommand("12345678912346789", "Test User", "test@example.com");
            var userDto = new UserDto { SteamId = createUserCommand.SteamId, DisplayName = createUserCommand.DisplayName };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>())).ReturnsAsync(new CreateUserOutput(userDto, null));

            // Act
            var result = await _controller.CreateUserAsync(createUserCommand);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.CreateUserAsync), createdAtActionResult.ActionName);
            Assert.Equal("12345678912346789", ((UserDto)createdAtActionResult.Value).SteamId);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsConflict_WhenUserCreationFails()
        {
            // Arrange
            var createUserCommand = new CreateUserCommand("12345678912346789", "Test User", "test@example.com");
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>())).ReturnsAsync(new CreateUserOutput(null, "User already exists"));

            // Act
            var result = await _controller.CreateUserAsync(createUserCommand);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("User already exists", conflictResult.Value);
        }
    }
}
