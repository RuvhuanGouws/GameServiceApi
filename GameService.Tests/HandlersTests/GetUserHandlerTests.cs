using GameService.Application.Handlers;
using GameService.Application.Queries;
using GameService.Infrastructure.Persistence;
using GameService.Domain.Entities;

namespace GameService.Tests.HandlersTests
{
    public class GetUserHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly GetUserBySteamIdHandler _getUserHandler;

        public GetUserHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _getUserHandler = new GetUserBySteamIdHandler(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidSteamId_ReturnsUser()
        {
            // Arrange
            var steamId = "123456789123456789";
            var user = new User("DisplayName", "email@example.com", steamId, Guid.NewGuid().ToString());
            _mockUserRepository.Setup(repo => repo.GetBySteamIdAsync(steamId))
                .ReturnsAsync(user);

            // Act
            var result = await _getUserHandler.Handle(new GetUserBySteamIdQuery(steamId));

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
            var result = await _getUserHandler.Handle(new GetUserBySteamIdQuery(steamId));

            // Assert
            Assert.Null(result);
        }
    }

}
