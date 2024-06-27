using Microsoft.AspNetCore.Mvc;
using GameService.Application.Queries;
using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Mediator;
using GameService.Domain.Entities;

namespace GameService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(string id)
        {
            var userDto = await _mediator.Send(new GetUserByIdQuery(id));
            return userDto.User == null ? NotFound() : Ok(userDto);
        }

        [HttpGet("Steam/{steamId}")]
        public async Task<ActionResult<UserDto>> GetUserBySteamIdAsync(string steamId)
        {
            try
            {
                var userDto = await _mediator.Send(new GetUserBySteamIdQuery(steamId));
                return userDto == null ? NotFound() : Ok(userDto);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserCommand command)
        {
            try
            {
                var output = await _mediator.Send(command);

                return output.IsSuccess ? CreatedAtAction(nameof(CreateUserAsync), new { steamId = output.User!.SteamId }, output.User) : Conflict(output.ErrorMessage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user." + ex.Message);
            }
        }
    }
}
