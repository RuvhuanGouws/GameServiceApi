using GameService.Domain.ValueObjects;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Net;
using User = GameService.Domain.Entities.User;

namespace GameService.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly Container _container;

        public UserRepository(Container container)
        {
            _container = container;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");
                FeedIterator<User> queryResultSetIterator = _container.GetItemQueryIterator<User>(queryDefinition);

                List<User> results = new List<User>();

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    results.AddRange(currentResultSet.Resource);
                }

                return results;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<User?> CreateAsync(string steamId, string email, string displayName)
        {
            var user = new User(displayName, email, steamId, Guid.NewGuid().ToString());
            var result = await _container.CreateItemAsync(user);
             
            return result.StatusCode == HttpStatusCode.Created ? user : null;
        }

        public async Task<User?> GetBySteamIdAsync(SteamId steamId)
        {
            try
            {
                QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.SteamId = @steamId")
                .WithParameter("@steamId", steamId.Value);
                FeedIterator<User> queryResultSetIterator = _container.GetItemQueryIterator<User>(queryDefinition);

                FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                if (currentResultSet.Count == 0)
                {
                    return null;
                }

                return currentResultSet.FirstOrDefault();
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            try
            {
                QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", id);
                FeedIterator<User> queryResultSetIterator = _container.GetItemQueryIterator<User>(queryDefinition);

                FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                if (currentResultSet.Count == 0)
                {
                    return null;
                }

                return currentResultSet.FirstOrDefault();
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
