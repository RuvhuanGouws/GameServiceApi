﻿using GameService.Application.Boundaries;
using GameService.Domain.Entities;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Queries
{
    public class GetUserBySteamIdQuery : IRequest<GetUserOutput>
    {
        public SteamId SteamId { get; }

        public GetUserBySteamIdQuery(SteamId steamId)
        {
            SteamId = steamId;
        }
    }
}
