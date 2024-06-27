using System.Text.Json.Serialization;

namespace GameService.Infrastructure.SteamApi.Models.GamesResponse
{
    public class Game
    {
        public int Appid { get; set; }
        public string Name { get; set; }
        public int PlaytimeForever { get; set; }
        [JsonPropertyName("img_icon_url")]
        public string ImgIconUrl { get; set; }
        public bool HasCommunityVisibleStats { get; set; }
    }
}
