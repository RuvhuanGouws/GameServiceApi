using GameService.Application.DTOs;
using GameService.Domain.Entities;
using GameService.Infrastructure.SteamApi;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GameService.Application.Mediators
{
    public class GameMediator : IGameMediator
    {
        private readonly ISteamApiClient _steamApiClient;

        public GameMediator(ISteamApiClient steamApiClient)
        {
            _steamApiClient = steamApiClient;
        }

        public async Task<IEnumerable<GameDto>> GetGamesForUser(string steamId)
        {
            var games = await _steamApiClient.GetOwnedGames(steamId);
            return games.Select(g => new GameDto
            {
                AppId = g.Appid,
                Name = g.Name,
                ImageUrl = $"http://media.steampowered.com/steamcommunity/public/images/apps/{g.Appid}/{g.ImgIconUrl}.jpg",
                HasCommunityVisibleStats = g.HasCommunityVisibleStats,
                PlaytimeForever = g.PlaytimeForever,
            });
        }

        public async Task<GameSchemaDto> GetGameDetails(int appId)
        {
            var game = await _steamApiClient.GetAppDetails(appId);
            List<GameSchemaAchievement> achievements = new List<GameSchemaAchievement>();

            // Not all games have achievements
            if (game.AvailableGameStats != null && game.AvailableGameStats.Achievements != null)
            {
                foreach (var achievement in game.AvailableGameStats.Achievements)
                {
                    achievements.Add(new GameSchemaAchievement
                    {
                        Defaultvalue = achievement.Defaultvalue,
                        Description = achievement.Description,
                        DisplayName = achievement.DisplayName,
                        Hidden = achievement.Hidden,
                        Icon = achievement.Icon,
                        IconGray = achievement.IconGray,
                        Name = achievement.Name
                    });
                }
            }
            
            return new GameSchemaDto
            {
                GameName = game.GameName,
                GameVersion = game.GameVersion,
                AvailableGameStats = new AvailableGameStats
                {
                    Achievements = achievements
                }
            };
        }
    }
}
