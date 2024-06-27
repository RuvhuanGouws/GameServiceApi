namespace GameService.Infrastructure.SteamApi.Models.GamesResponse
{
    public class GamesResponse
    {
        public int Game_Count { get; set; }
        public List<Game> Games { get; set; }
    }
}
