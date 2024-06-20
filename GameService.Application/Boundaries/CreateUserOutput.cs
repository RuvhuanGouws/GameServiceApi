using GameService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class CreateUserOutput
    {
        public UserDto User { get; }

        public CreateUserOutput(UserDto user)
        {
            User = user;
        }
    }
}
