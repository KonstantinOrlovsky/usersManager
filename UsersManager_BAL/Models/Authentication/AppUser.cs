using UsersManager_BAL.Contracts.Models.Authentication;

namespace UsersManager_BAL.Models.Authentication
{
    public class AppUser : IAppUser
    {
        public AppUser(Guid id, string token = "", string refreshToken = default)
        {
            Id = id;
            Token = token;
            RefreshToken = refreshToken;
        }

        public Guid Id { get; }
        public string? Token { get; }
        public string? RefreshToken { get; }
    }
}