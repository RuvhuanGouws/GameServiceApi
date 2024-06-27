using GameService.Application.DTOs.GameSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class GetGameDetailsOutput
    {
        public GameSchemaDto GameDetails { get; }

        public GetGameDetailsOutput(GameSchemaDto gameDetails)
        {
            GameDetails = gameDetails;
        }
    }
}
