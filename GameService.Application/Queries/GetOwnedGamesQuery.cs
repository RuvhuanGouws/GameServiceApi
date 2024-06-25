using GameService.Application.Boundaries;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Queries
{
    public class GetOwnedGamesQuery : IRequest<GetOwnedGamesOutput>
    {
        public SteamId SteamId { get; }

        public GetOwnedGamesQuery(SteamId steamId)
        {
            SteamId = steamId;
        }
    }
}
