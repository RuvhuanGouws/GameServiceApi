using GameService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class GetUserOutput
    {
        public UserDto User { get; }

        public GetUserOutput(UserDto user)
        {
            User = user;
        }
    }
}
