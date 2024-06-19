using Microsoft.AspNetCore.Mvc;
using GameService.Application.Mediators;
using GameService.Application.Queries;
using GameService.Application.Commands;
using GameService.Application.DTOs;
using System.Diagnostics;

namespace GameService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserMediator _userMediator;

        public UserController(IUserMediator userMediator)
        {
            _userMediator = userMediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userMediator.Handle();
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("{steamId}")]
        public async Task<ActionResult<UserDto>> GetUser(string steamId)
        {
            var userDto = await _userMediator.Handle(new GetUserQuery(steamId));
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUser = await _userMediator.Handle(new GetUserQuery(dto.SteamId));
                if (existingUser != null)
                {
                    return Conflict("User with this Steam ID already exists.");
                }

                var command = new CreateUserCommand(dto.SteamId, dto.DisplayName, dto.Email, Guid.NewGuid());
                await _userMediator.Handle(command);

                return CreatedAtAction(nameof(GetUser), new { steamId = dto.SteamId.Value }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user." + ex.Message);
            }
        }
    }
}
