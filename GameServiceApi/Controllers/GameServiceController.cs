using Microsoft.AspNetCore.Mvc;
using GameService.Domain.Entities;
using GameService.Application.DTOs;
using GameService.Application.Mediator;
using GameService.Application.Queries;
using GameService.Domain.ValueObjects;
using Microsoft.Azure.Cosmos.Linq;
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
        public async Task<ActionResult<IEnumerable<GameDto>>> GetOwnedGames(string steamId)
        {
            var games = await _mediator.Send(new GetOwnedGamesQuery(steamId));
            return games.Games == null ? NotFound() : Ok(games.Games);
        }

        [HttpGet("{appId}")]
        public async Task<ActionResult<GameSchemaDto>> GetGameDetails(int appId)
        {
            var game = await _mediator.Send(new GetGameDetailsQuery(appId));
            return game == null ? NotFound() : Ok(game.GameDetails);
        }
    }
}
