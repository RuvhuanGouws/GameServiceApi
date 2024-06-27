using GameService.Application.Mappers;
using GameService.Domain.Entities;


namespace GameService.Tests.MappersTests
{
    public class UserMapperTests
    {
        [Fact]
        public void ToDto_ValidUser_ReturnsExpectedUserDto()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestDisplayName", "test@example.com", "123456789123456789");

            // Act
            var result = UserMapper.ToDto(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.SteamId, result.SteamId);
            Assert.Equal(user.DisplayName, result.DisplayName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.id, result.Id);
        }

        [Fact]
        public void ToDto_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            User user = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => UserMapper.ToDto(user));
        }
    }
}
