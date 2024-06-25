using GameService.Application.DTOs;
using GameService.Infrastructure.SteamApi.Models;

namespace GameService.Application.Mappers
{
    public static class GameMapper
    {
        public static GameDto ToDto(Game game)
        {
            return new GameDto(
                game.Appid,
                game.Name,
                $"http://media.steampowered.com/steamcommunity/public/images/apps/{game.Appid}/{game.ImgIconUrl}.jpg", // ImgIconUrl only contains a hash with file name
                game.PlaytimeForever,
                game.HasCommunityVisibleStats);
        }
    }
}
