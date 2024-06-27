using Microsoft.AspNetCore.Mvc;
using GameService.Application.Queries;
using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Mediator;

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
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("{steamId}")]
        public async Task<ActionResult<UserDto>> GetUser(string steamId)
        {
            try
            {
                var userDto = await _mediator.Send(new GetUserQuery(steamId));
                return userDto == null ? NotFound() : Ok(userDto);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            try
            {
                var command = new CreateUserCommand(dto.SteamId, dto.DisplayName, dto.Email, Guid.NewGuid());
                var output = await _mediator.Send(command);

                return output.IsSuccess ? CreatedAtAction(nameof(GetUser), new { steamId = output.User!.SteamId }, output.User) : Conflict(output.ErrorMessage);
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
