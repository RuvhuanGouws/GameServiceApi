using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Commands
{
    public class CreateUserCommand
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public SteamId SteamId { get; set; }

        public CreateUserCommand(SteamId steamId, string displayName, string email, Guid id)
        {
            Id = id;
            SteamId = steamId;
            DisplayName = displayName;
            Email = email;
        }
    }
}
