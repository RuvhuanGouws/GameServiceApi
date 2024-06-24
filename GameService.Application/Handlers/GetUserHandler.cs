using GameService.Application.Boundaries;
using GameService.Application.Mappers;
using GameService.Application.Queries;
using GameService.Domain.Entities;
using GameService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserOutput>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserOutput?> Handle(GetUserQuery request)
        {
            var user = await _userRepository.GetBySteamIdAsync(request.SteamId);
            return user != null ? new GetUserOutput(UserMapper.ToDto(user)) : null;
        }
    }
}
