using GameService.Application.Boundaries;
using GameService.Application.DTOs.GameSchema;
using GameService.Application.Mappers;
using GameService.Application.Queries;
using GameService.Domain.ValueObjects;
using GameService.Infrastructure.SteamApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Handlers
{
    public class GetGameDetailsHandler : IRequestHandler<GetGameDetailsQuery, GetGameDetailsOutput>
    {
        private readonly ISteamApiClient _steamApiClient;

        public GetGameDetailsHandler(ISteamApiClient steamApiClient)
        {
            _steamApiClient = steamApiClient;
        }

        public async Task<GetGameDetailsOutput?> Handle(GetGameDetailsQuery request)
        {
            var game = await _steamApiClient.GetAppDetails(request.AppId);


            if (game == null)
            {
                return null;
            }

            List<GameSchemaAchievementDto> achievements = new List<GameSchemaAchievementDto>();

            // Not all games have achievements
            if (game.AvailableGameStats != null && game.AvailableGameStats.Achievements != null)
            {
                foreach (var achievement in game.AvailableGameStats.Achievements)
                {
                    achievements.Add(new GameSchemaAchievementDto
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

            return new GetGameDetailsOutput(new GameSchemaDto
            {
                GameName = game.GameName,
                GameVersion = game.GameVersion,
                AvailableGameStats = new AvailableGameStatsDto
                {
                    Achievements = achievements
                }
            });
        }
    }
}
