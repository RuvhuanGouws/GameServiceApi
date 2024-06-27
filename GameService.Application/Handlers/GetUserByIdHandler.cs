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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserOutput>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserOutput?> Handle(GetUserByIdQuery request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            return user != null ? new GetUserOutput(UserMapper.ToDto(user)) : null;
        }
    }
}
