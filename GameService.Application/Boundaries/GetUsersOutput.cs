using GameService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class GetUsersOutput
    {
        public List<UserDto> Users { get; }

        public GetUsersOutput(List<UserDto> users)
        {
            Users = users;
        }
    }
}
