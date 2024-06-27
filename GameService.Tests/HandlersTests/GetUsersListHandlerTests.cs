using GameService.Application.Handlers;
using GameService.Application.Queries;
using GameService.Infrastructure.Persistence;
using GameService.Domain.Entities;

namespace GameService.Tests.HandlersTests
{
    

    public class GetUsersListHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly GetUsersListHandler _getUsersListHandler;

        public GetUsersListHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _getUsersListHandler = new GetUsersListHandler(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User(Guid.NewGuid(), "User1", "user1@example.com", "123456789123456789"),
                new User(Guid.NewGuid(), "User2", "user2@example.com", "123456789123456788")
            };
            _mockUserRepository.Setup(repo => repo.GetUsers()).ReturnsAsync(users);

            // Act
            var result = await _getUsersListHandler.Handle(new GetUsersQuery());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Users.Count);
            for (int i = 0; i < users.Count; i++)
            {
                Assert.Equal(users[i].DisplayName, result.Users[i].DisplayName);
                Assert.Equal(users[i].Email, result.Users[i].Email);
                Assert.Equal(users[i].SteamId, result.Users[i].SteamId);
            }
        }

        [Fact]
        public async Task Handle_NoUsersExist_ReturnsEmptyList()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUsers()).ReturnsAsync(new List<User>());

            // Act
            var result = await _getUsersListHandler.Handle(new GetUsersQuery());

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Users);
        }
    }

}
