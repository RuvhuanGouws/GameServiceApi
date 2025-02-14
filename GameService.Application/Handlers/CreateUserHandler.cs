using GameService.Application.Boundaries;
using GameService.Application.Commands;
using GameService.Application.Mappers;
using GameService.Infrastructure.Persistence;
using GameService.Infrastructure.SteamApi;
using System.Text.RegularExpressions;

namespace GameService.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserOutput>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISteamApiClient _steamApiClient;

        public CreateUserHandler(IUserRepository userRepository, ISteamApiClient steamApiClient)
        {
            _userRepository = userRepository;
            _steamApiClient = steamApiClient;
        }

        public async Task<CreateUserOutput?> Handle(CreateUserCommand request)
        {
            if (!Regex.IsMatch(request.Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"))
            {
                return new CreateUserOutput(null, "Email is not valid.");
            }

            var userSummary = await _steamApiClient.GetOwnedGames(request.SteamId);

            if (userSummary == null)
            {
                return new CreateUserOutput(null, "This is not a registered SteamID or the Steam profile is set to private.");
            }

            var existingUser = await _userRepository.GetBySteamIdAsync(request.SteamId);

            if (existingUser != null)
            {
                return new CreateUserOutput(null, "User with that SteamId already exists.");
            }

            var user = await _userRepository.CreateAsync(request.SteamId, request.Email, request.DisplayName);

            if (user == null)
            {
                return null;
            }

            return new CreateUserOutput(UserMapper.ToDto(user), null);
        }
    }
}
