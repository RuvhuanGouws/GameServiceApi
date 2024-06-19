using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Queries;

namespace GameService.Application.Mediators
{
    public interface IUserMediator
    {
        Task Handle(CreateUserCommand command);
        Task<UserDto?> Handle(GetUserQuery query);
        Task<List<UserDto>?> Handle();
    }
}
