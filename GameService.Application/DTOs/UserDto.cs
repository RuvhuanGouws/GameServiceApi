using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.DTOs
{
    public class UserDto
    {
        String Id { get; set; }
        public SteamId SteamId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        

        public UserDto() 
        {
            SteamId = "76561198070790326";
            DisplayName = string.Empty;
            Email = string.Empty;            
        }

        public UserDto(SteamId steamId, string displayName, string email, string id)
        {
            SteamId = steamId;
            DisplayName = displayName;
            Email = email;
            Id = id;
        }
    }
}
