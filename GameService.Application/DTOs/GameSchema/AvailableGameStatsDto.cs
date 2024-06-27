using GameService.Infrastructure.SteamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.DTOs.GameSchema
{
    public class AvailableGameStatsDto
    {
        public List<GameSchemaAchievementDto> Achievements { get; set; }
    }
}
