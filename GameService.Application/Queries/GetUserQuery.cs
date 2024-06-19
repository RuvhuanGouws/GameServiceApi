using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Queries
{
    public class GetUserQuery
    {
        public SteamId SteamId { get; }

        public GetUserQuery(SteamId steamId)
        {
            SteamId = steamId;
        }
    }
}
