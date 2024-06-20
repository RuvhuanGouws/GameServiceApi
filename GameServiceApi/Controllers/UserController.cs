using Microsoft.AspNetCore.Mvc;
using GameService.Application.Queries;
using GameService.Application.Commands;
using GameService.Application.DTOs;
using System.Diagnostics;
using GameService.Application.Services;

namespace GameService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync(new GetUsersQuery());
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("{steamId}")]
        public async Task<ActionResult<UserDto>> GetUser(string steamId)
        {
            var userDto = await _userService.GetUserAsync(new GetUserQuery(steamId));
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

                var existingUser = await _userService.GetUserAsync(new GetUserQuery(dto.SteamId));
                if (existingUser != null)
                {
                    return Conflict("User with this Steam ID already exists.");
                }

                var command = new CreateUserCommand(dto.SteamId, dto.DisplayName, dto.Email, Guid.NewGuid());
                var output = await _userService.CreateUserAsync(command);

                return CreatedAtAction(nameof(GetUser), new { steamId = output.User.SteamId.Value }, output.User);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user." + ex.Message);
            }
        }
    }
}
