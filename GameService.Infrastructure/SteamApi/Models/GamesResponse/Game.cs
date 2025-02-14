using System.Text.Json.Serialization;

namespace GameService.Infrastructure.SteamApi.Models.GamesResponse
{
    public class Game
    {
        [JsonPropertyName("appid")]
        public int Appid { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("playtime_forever")]
        public int PlaytimeForever { get; set; }
        [JsonPropertyName("img_icon_url")]
        public string ImgIconUrl { get; set; }
        [JsonPropertyName("has_community_visible_stats ")]
        public bool HasCommunityVisibleStats { get; set; }
    }
}
