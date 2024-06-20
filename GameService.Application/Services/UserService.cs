using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Queries;
using GameService.Application.Boundaries;
using GameService.Application.Mediator;

namespace GameService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateUserOutput> CreateUserAsync(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserDto?> GetUserAsync(GetUserQuery query)
        {
            var output = await _mediator.Send(query);
            if (output == null)
            {
                return null;
            }
            return output.User;
        }

        public async Task<List<UserDto>?> GetUsersAsync(GetUsersQuery query)
        {
            var output = await _mediator.Send(query);
            if (output == null)
            {
                return null;
            }
            return output.Users;
        }
    }
}
