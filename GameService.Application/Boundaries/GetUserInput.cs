using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class GetUserInput
    {
        public SteamId SteamId { get; }

        public GetUserInput(SteamId steamId)
        {
            SteamId = steamId;
        }
    }
}
