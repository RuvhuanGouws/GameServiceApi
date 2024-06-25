using GameService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class GetOwnedGamesOutput
    {
        public IEnumerable<GameDto> Games { get; }

        public GetOwnedGamesOutput(IEnumerable<GameDto> games)
        {
            Games = games;
        }
    }
}
