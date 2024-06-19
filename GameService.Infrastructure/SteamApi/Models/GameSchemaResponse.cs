using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameService.Infrastructure.SteamApi.Models
{
    public class GameSchemaAchievement
    {
        public string Name { get; set; }
        public int Defaultvalue { get; set; }
        public string DisplayName { get; set; }
        public int Hidden { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconGray { get; set; }
    }

    public class AvailableGameStats
    {
        public List<GameSchemaAchievement> Achievements { get; set; }
    }

    public class GameSchemaResponse
    {
        public string GameName { get; set; }

        public string GameVersion { get; set; }

        public AvailableGameStats AvailableGameStats { get; set; }
    }

    public class GameSchemaRoot
    {
        public GameSchemaResponse Game { get; set; }
    }
}
