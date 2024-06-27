﻿namespace GameService.Infrastructure.SteamApi.Models.GameSchemaResponse
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
}
