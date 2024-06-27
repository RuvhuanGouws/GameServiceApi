using GameService.Application.Boundaries;
using GameService.Application.Queries;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Commands
{
    public class CreateUserCommand : IRequest<CreateUserOutput>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string SteamId { get; set; }

        public CreateUserCommand(string steamId, string displayName, string email)
        {
            SteamId = steamId;
            DisplayName = displayName;
            Email = email;
        }
    }
}
