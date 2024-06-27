using GameService.Infrastructure.SteamApi.Models;
using GameService.Application.Mappers;

namespace GameService.Tests.MappersTests
{
    

    public class GameMapperTests
    {
        [Fact]
        public void ToDto_ValidGame_ReturnsExpectedGameDto()
        {
            // Arrange
            var game = new Game
            {
                Appid = 123,
                Name = "Test Game",
                ImgIconUrl = "abc123",
                PlaytimeForever = 100,
                HasCommunityVisibleStats = true
            };

            var expectedImageUrl = "http://media.steampowered.com/steamcommunity/public/images/apps/123/abc123.jpg";

            // Act
            var result = GameMapper.ToDto(game);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(game.Appid, result.AppId);
            Assert.Equal(game.Name, result.Name);
            Assert.Equal(expectedImageUrl, result.ImageUrl);
            Assert.Equal(game.PlaytimeForever, result.PlaytimeForever);
            Assert.Equal(game.HasCommunityVisibleStats, result.HasCommunityVisibleStats);
        }

        [Fact]
        public void ToDto_GameWithNullImgIconUrl_ReturnsGameDtoWithDefaultImageUrl()
        {
            // Arrange
            var game = new Game
            {
                Appid = 123,
                Name = "Test Game",
                ImgIconUrl = null, // Simulate a game without an icon URL
                PlaytimeForever = 100,
                HasCommunityVisibleStats = true
            };

            var expectedImageUrl = string.Empty; // Expected default behavior

            // Act
            var result = GameMapper.ToDto(game);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedImageUrl, result.ImageUrl); // Verify the default image URL handling
        }

        [Fact]
        public void ToDto_GameWithEmptyFields_ReturnsGameDtoWithEmptyFields()
        {
            // Arrange
            var game = new Game
            {
                Appid = 0,
                Name = "",
                ImgIconUrl = "",
                PlaytimeForever = 0,
                HasCommunityVisibleStats = false
            };

            var expectedImageUrl = string.Empty; // Expected behavior for empty ImgIconUrl

            // Act
            var result = GameMapper.ToDto(game);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.AppId);
            Assert.Equal("", result.Name);
            Assert.Equal(expectedImageUrl, result.ImageUrl);
            Assert.Equal(0, result.PlaytimeForever);
            Assert.False(result.HasCommunityVisibleStats);
        }
    }
}
