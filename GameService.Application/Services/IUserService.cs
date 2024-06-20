using GameService.Application.Boundaries;
using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Queries;

namespace GameService.Application.Services
{
    public interface IUserService
    {
        public Task<CreateUserOutput> CreateUserAsync(CreateUserCommand command);
        public Task<UserDto?> GetUserAsync(GetUserQuery query);
        public Task<List<UserDto>?> GetUsersAsync(GetUsersQuery query);
    }
}
