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
        public bool IsSuccess => User != null;
        public string? ErrorMessage { get; set; }
        public UserDto? User { get; }

        public CreateUserOutput(UserDto? user, string? errorMessage)
        {
            User = user;
            ErrorMessage = errorMessage;
        }
    }
}
