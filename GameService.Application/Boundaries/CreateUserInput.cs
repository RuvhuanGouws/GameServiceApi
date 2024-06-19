using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Boundaries
{
    public class CreateUserInput
    {
        public Guid Id { get; }
        public SteamId SteamId { get; }
        public string DisplayName { get; }
        public string Email { get; }

        public CreateUserInput(Guid id, SteamId steamId, string displayName, string email)
        {
            Id = id;
            SteamId = steamId;
            DisplayName = displayName;
            Email = email;
        }
    }
}
