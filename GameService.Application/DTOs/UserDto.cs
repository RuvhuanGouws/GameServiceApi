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
        public string Id { get; set; }
        public string SteamId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        

        public UserDto() 
        {
            SteamId = "123456789123456789";
            DisplayName = string.Empty;
            Email = string.Empty;
            Id = string.Empty;
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
