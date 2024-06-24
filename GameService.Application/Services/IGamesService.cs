﻿using GameService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Services
{
    public interface IGamesService
    {
        Task<IEnumerable<GameDto>> GetGamesForUser(string steamId);
        Task<GameSchemaDto> GetGameDetails(int appId);
    }
}