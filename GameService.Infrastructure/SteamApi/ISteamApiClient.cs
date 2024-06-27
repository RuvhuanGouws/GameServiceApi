using GameService.Infrastructure.SteamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.SteamApi
{
    public interface ISteamApiClient
    {
        Task<List<Game>?> GetOwnedGames(string steamId);
        Task<GameSchemaResponse?> GetAppDetails(int appId);
    }
}
