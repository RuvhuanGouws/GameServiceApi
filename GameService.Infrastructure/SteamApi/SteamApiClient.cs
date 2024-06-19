using GameService.Domain.ValueObjects;
using GameService.Infrastructure.SteamApi.Models;
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

        public SteamApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("STEAM_API_KEY")
              ?? throw new Exception("STEAM_API_KEY environment variable not found.");
        }
        public async Task<GameSchemaResponse> GetAppDetails(int appId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?key=3B95FB9C45C065A2A6F4D6EDDB5B6791&appid={appId}");
            var responset = await client.SendAsync(request);
            var jsonResponse = await responset.Content.ReadAsStringAsync();
            var response = await _httpClient.GetFromJsonAsync<GameSchemaRoot>(
                $"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?key={_apiKey}&appid={appId}");

            if (response is null || response.Game == null)
            {
                throw new Exception("Failed to retrieve game schema details.");
            }

            return response.Game!;
        }

        public async Task<IEnumerable<Game>> GetOwnedGames(string steamId)
        {
            var response = await _httpClient.GetFromJsonAsync<GamesResponseRoot>(
                $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={steamId}&format=json&include_appinfo=true&include_played_free_games=true");

            if (response is null || !response.Response.Games.Any())
            {
                throw new Exception("Failed to retrieve owned games for the given steam ID.");
            }

            return response.Response.Games;
        }
    }
}
