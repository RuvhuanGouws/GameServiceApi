﻿using Microsoft.AspNetCore.Mvc;
using GameService.Domain.Entities;
using GameService.Application.DTOs;
using GameService.Application.Services;
namespace GameService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameServiceController : ControllerBase
    {
        private readonly IGamesService _mediator;

        public GameServiceController(IGamesService mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{steamId}")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesForUser(string steamId)
        {
            var games = await _mediator.GetGamesForUser(steamId);
            return Ok(games);
        }

        [HttpGet("{appId}")]
        public async Task<ActionResult<GameSchemaDto>> GetGameDetails(int appId)
        {
            var game = await _mediator.GetGameDetails(appId);
            return Ok(game);
        }
    }
}
