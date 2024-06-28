using GameService.Domain.ValueObjects;
using GameService.Infrastructure.SteamApi.Models.GameSchemaResponse;
using GameService.Infrastructure.SteamApi.Models.GamesResponse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.SteamApi
{
    public class SteamApiClient : ISteamApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string SteamBaseUri = "https://api.steampowered.com";

        public SteamApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("STEAM_API_KEY")
              ?? throw new Exception("STEAM_API_KEY environment variable not found.");
        }
        public async Task<GameSchemaResponse?> GetAppDetails(int appId)
        {
            var response = await _httpClient.GetFromJsonAsync<GameSchemaRoot>(
                $"{SteamBaseUri}/ISteamUserStats/GetSchemaForGame/v2/?key={_apiKey}&appid={appId}");

            if (response is null || response.Game == null)
            {
                return null;
            }

            return response.Game!;
        }

        public async Task<List<Game>?> GetOwnedGames(string steamId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<GamesResponseRoot>(
                $"{SteamBaseUri}/IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={steamId}&format=json&include_appinfo=true&include_played_free_games=true");

                if (response is null || !response.Response.Games.Any())
                {
                    return null;
                }

                return response.Response.Games;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
