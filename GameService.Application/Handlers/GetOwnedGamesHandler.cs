using GameService.Application.Boundaries;
using GameService.Application.DTOs;
using GameService.Application.Mappers;
using GameService.Application.Queries;
using GameService.Infrastructure.SteamApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Handlers
{
    public class GetOwnedGamesHandler : IRequestHandler<GetOwnedGamesQuery, GetOwnedGamesOutput>
    {
        private readonly ISteamApiClient _steamApiClient;

        public GetOwnedGamesHandler(ISteamApiClient steamApiClient)
        {
            _steamApiClient = steamApiClient;
        }

        public async Task<GetOwnedGamesOutput?> Handle(GetOwnedGamesQuery request)
        {
            var games = await _steamApiClient.GetOwnedGames(request.SteamId);

            if (games == null)
            {
                return null;
            }

            return new GetOwnedGamesOutput(games.Select(g => GameMapper.ToDto(g)));
        }
    }
}
