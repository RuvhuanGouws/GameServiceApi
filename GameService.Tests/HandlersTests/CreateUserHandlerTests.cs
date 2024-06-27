using GameService.Application.Handlers;
using GameService.Application.Commands;
using GameService.Infrastructure.Persistence;
using GameService.Domain.Entities;
using System.Net;

namespace GameService.Tests.HandlersTests
{
    public class CreateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly CreateUserHandler _createUserHandler;

        public CreateUserHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _createUserHandler = new CreateUserHandler(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_CreatesUser()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "test@example.com", Guid.NewGuid());

            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync((User)null);

            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(HttpStatusCode.Created);

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorMessage);
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserExists_ReturnsErrorMessage()
        {
            // Arrange
            var command = new CreateUserCommand("123456789123456789", "Existing User", "existing@example.com", Guid.NewGuid());

            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync(new User(command.Id, command.DisplayName, command.Email, command.SteamId));

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User with that SteamId already exists.", result.ErrorMessage);
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_CreationFails_ReturnsNull()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "test@example.com", Guid.NewGuid());

            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync((User)null);

            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(HttpStatusCode.InternalServerError); // Simulate creation failure

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.Null(result);
        }
    }
}
