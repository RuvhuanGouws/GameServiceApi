using GameService.Application.Boundaries;
using GameService.Application.Mappers;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.UseCases
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserOutput?> Execute(GetUserInput input)
        {
            var user = await _userRepository.GetBySteamIdAsync(input.SteamId);
            return user != null ? new GetUserOutput(UserMapper.ToDto(user)) : null;
        }
    }
}
