using GameService.Application.Boundaries;
using GameService.Application.Commands;
using GameService.Application.Mappers;
using GameService.Domain.Entities;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserOutput>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserOutput?> Handle(CreateUserCommand request)
        {
            var user = new User(request.Id, request.DisplayName, request.Email, request.SteamId);

            var statusCode = await _userRepository.CreateAsync(user);

            if (statusCode != System.Net.HttpStatusCode.Created)
            {
                return null;
            }

            return new CreateUserOutput(UserMapper.ToDto(user));
        }
    }
}
