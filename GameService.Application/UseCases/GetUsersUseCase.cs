using GameService.Application.Boundaries;
using GameService.Application.DTOs;
using GameService.Application.Mappers;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.UseCases
{
    public class GetUsersUseCase : IGetUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUsersOutput?> Execute()
        {
            var users = await _userRepository.GetUsers();

            var usersOutput = new List<UserDto>();

            foreach (var user in users)
            {
                usersOutput.Add(UserMapper.ToDto(user));
            }

            return usersOutput != null ? new GetUsersOutput(usersOutput) : null;
        }
    }
}
