namespace GameService.Infrastructure.SteamApi.Models.GameSchemaResponse
{
    public class GameSchemaResponse
    {
        public string GameName { get; set; }

        public string GameVersion { get; set; }

        public AvailableGameStats AvailableGameStats { get; set; }
    }
}
