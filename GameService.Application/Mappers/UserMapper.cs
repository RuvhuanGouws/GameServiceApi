using GameService.Application.DTOs;
using GameService.Domain.Entities;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto(user.SteamId, user.DisplayName, user.Email, user.id);
        }
    }
}
