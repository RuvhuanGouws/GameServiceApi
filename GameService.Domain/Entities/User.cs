using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Domain.Entities
{
    public class User
    {
        public string? id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string SteamId { get; set; }

        public User(string displayName, string email, string steamId, string userId = "")
        {
            id = userId;
            DisplayName = displayName;
            Email = email;
            SteamId = steamId;
        }
    }
}
