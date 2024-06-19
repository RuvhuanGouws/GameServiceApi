using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameService.Infrastructure.SteamApi.Models
{
    public class GamesResponse
    {
        public int Game_Count { get; set; }
        public List<Game> Games { get; set; }
    }

    public class Game
    {
        public int Appid { get; set; }
        public string Name { get; set; }
        public int PlaytimeForever { get; set; }
        [JsonPropertyName("img_icon_url")]
        public string ImgIconUrl { get; set; }
        public bool HasCommunityVisibleStats { get; set; }
    }

    public class GamesResponseRoot
    {
        public GamesResponse Response { get; set; }
    }
}
