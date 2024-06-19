using GameService.Domain.Entities;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Persistence
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task CreateAsync(User user);
        public Task<User?> GetBySteamIdAsync(SteamId steamId);
    }
}
