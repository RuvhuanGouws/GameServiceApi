using GameService.Domain.Entities;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Persistence
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User?> CreateAsync(string steamId, string email, string displayName);
        public Task<User?> GetBySteamIdAsync(SteamId steamId);
        public Task<User?> GetByIdAsync(string id);
    }
}
