using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.DTOs
{
    public class GameDto
    {
        public int AppId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int PlaytimeForever { get; set; }
        public bool HasCommunityVisibleStats { get; set; }

        public GameDto(int appId, string name, string imageUrl, int playtimeForever, bool hasCommunityVisibleStats)
        {
            AppId = appId;
            Name = name;
            ImageUrl = imageUrl;
            PlaytimeForever = playtimeForever;
            HasCommunityVisibleStats = hasCommunityVisibleStats;
        }
    }
}
