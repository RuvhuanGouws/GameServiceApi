using GameService.Application.Boundaries;
using GameService.Domain.Entities;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.UseCases
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(CreateUserInput input)
        {
            var user = new User(input.Id, input.DisplayName, input.Email, input.SteamId);

            await _userRepository.CreateAsync(user);
        }
    }
}
