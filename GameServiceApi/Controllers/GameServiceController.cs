﻿using Microsoft.AspNetCore.Mvc;
using GameService.Application.DTOs;
using GameService.Application.Mediator;
using GameService.Application.Queries;
using GameService.Application.DTOs.GameSchema;
namespace GameService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{steamId}")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetOwnedGamesAsync(string steamId)
        {
            try
            {
                var games = await _mediator.Send(new GetOwnedGamesQuery(steamId));
                return games.Games == null ? NotFound() : Ok(games.Games);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{appId}")]
        public async Task<ActionResult<GameSchemaDto>> GetGameDetailsAsync(int appId)
        {
            try
            {
                var game = await _mediator.Send(new GetGameDetailsQuery(appId));
                return game == null ? NotFound() : Ok(game.GameDetails);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
