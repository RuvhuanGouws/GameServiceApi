using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.DTOs.GameSchema
{
    public class GameSchemaDto
    {
        public string GameName { get; set; }

        public string GameVersion { get; set; }

        public AvailableGameStatsDto AvailableGameStats { get; set; }
    }

    

    
}
