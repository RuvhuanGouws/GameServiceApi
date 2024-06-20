using GameService.Application.Boundaries;
using GameService.Application.DTOs;
using GameService.Application.Mappers;
using GameService.Application.Queries;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Handlers
{
    public class UsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersOutput>
    {
        private readonly IUserRepository _userRepository;

        public UsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUsersOutput?> Handle(GetUsersQuery request)
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
