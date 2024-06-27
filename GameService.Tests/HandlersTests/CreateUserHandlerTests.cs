using GameService.Application.Handlers;
using GameService.Application.Commands;
using GameService.Infrastructure.Persistence;
using GameService.Domain.Entities;
using GameService.Infrastructure.SteamApi;
using GameService.Infrastructure.SteamApi.Models.GamesResponse;

namespace GameService.Tests.HandlersTests
{
    public class CreateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISteamApiClient> _mockSteamApiClient; 
        private readonly CreateUserHandler _createUserHandler;

        public CreateUserHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSteamApiClient = new Mock<ISteamApiClient>();
            _createUserHandler = new CreateUserHandler(_mockUserRepository.Object, _mockSteamApiClient.Object);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_CreatesUser()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "test@example.com");

            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync((User?)null);

            _mockSteamApiClient.Setup(client => client.GetOwnedGames(command.SteamId))
                .ReturnsAsync(new List<Game>());

            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User(command.DisplayName, command.Email, command.SteamId, Guid.NewGuid().ToString()));

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorMessage);
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserExists_ReturnsErrorMessage()
        {
            // Arrange
            var command = new CreateUserCommand("123456789123456789", "Existing User", "existing@example.com");

            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync(new User(command.DisplayName, command.Email, command.SteamId, Guid.NewGuid().ToString()));

            _mockSteamApiClient.Setup(client => client.GetOwnedGames(command.SteamId))
                .ReturnsAsync(new List<Game>());

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User with that SteamId already exists.", result.ErrorMessage);
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_CreationFails_ReturnsNull()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "test@example.com");

            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync((User?)null);

            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User?)null); // Simulate creation failure by returning null

            _mockSteamApiClient.Setup(client => client.GetOwnedGames(command.SteamId))
                .ReturnsAsync(new List<Game>());

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_InvalidEmailFormat_ReturnsErrorMessage()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "invalid_email");

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Email is not valid.", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_SteamIdNotRegisteredOrPrivate_ReturnsErrorMessage()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "test@example.com");
            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync((User?)null);

            _mockSteamApiClient.Setup(client => client.GetOwnedGames(command.SteamId))
                .ReturnsAsync((List<Game>?)null);

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("This is not a registered SteamID or the Steam profile is set to private.", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ValidRequest_CreatesUserAndReturnsSuccess()
        {
            // Arrange
            var command = new CreateUserCommand("12345678912346789", "Test User", "test@example.com");
            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(command.SteamId))
                .ReturnsAsync((User?)null);

            _mockSteamApiClient.Setup(client => client.GetOwnedGames(command.SteamId))
                .ReturnsAsync(new List<Game> { new Game {
                    Appid = 123,
                    HasCommunityVisibleStats = true,
                    ImgIconUrl = "icon.jpg",
                    Name = "Test Game",
                    PlaytimeForever = 100
                }});

            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User(command.DisplayName, command.Email, command.SteamId, Guid.NewGuid().ToString()));

            // Act
            var result = await _createUserHandler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorMessage);
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

    }
}

