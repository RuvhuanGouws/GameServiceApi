using GameService.Application.Handlers;
using GameService.Application.Queries;
using GameService.Infrastructure.Persistence;
using GameService.Domain.Entities;

namespace GameService.Tests.HandlersTests
{
    public class GetUserHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly GetUserHandler _getUserHandler;

        public GetUserHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _getUserHandler = new GetUserHandler(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidSteamId_ReturnsUser()
        {
            // Arrange
            var steamId = "123456789123456789";
            var user = new User(Guid.NewGuid(), "DisplayName", "email@example.com", steamId);
            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(steamId))
                .ReturnsAsync(user);

            // Act
            var result = await _getUserHandler.Handle(new GetUserQuery(steamId));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.SteamId, result.User.SteamId);
            Assert.Equal(user.DisplayName, result.User.DisplayName);
            Assert.Equal(user.Email, result.User.Email);
        }

        [Fact]
        public async Task Handle_InvalidSteamId_ReturnsNull()
        {
            // Arrange
            var steamId = "999999999999999999";
            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(steamId))
                .ReturnsAsync((User)null);

            // Act
            var result = await _getUserHandler.Handle(new GetUserQuery(steamId));

            // Assert
            Assert.Null(result);
        }
    }

}
