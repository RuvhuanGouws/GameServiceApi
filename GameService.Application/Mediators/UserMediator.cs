using GameService.Application.Commands;
using GameService.Application.DTOs;
using GameService.Application.Queries;
using GameService.Application.UseCases;
using GameService.Application.Boundaries;
using GameService.Domain.Entities;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Mediators
{
    public class UserMediator : IUserMediator
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly IGetUsersUseCase _getUsersUseCase;

        public UserMediator(
            ICreateUserUseCase createUserUseCase,
            IGetUserUseCase getUserUseCase,
            IGetUsersUseCase getUsersUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _getUserUseCase = getUserUseCase;
            _getUsersUseCase = getUsersUseCase;
        }

        public async Task Handle(CreateUserCommand command)
        {
            var input = new CreateUserInput(command.Id, command.SteamId, command.DisplayName, command.Email);
            await _createUserUseCase.Execute(input);
        }

        public async Task<UserDto?> Handle(GetUserQuery query)
        {
            var input = new GetUserInput(query.SteamId);
            var output = await _getUserUseCase.Execute(input);
            return output != null ? output.User : null;
        }

        public async Task<List<UserDto>?> Handle()
        {
            var output = await _getUsersUseCase.Execute();
            return output != null ? output.Users : null;
        }
    }
}
