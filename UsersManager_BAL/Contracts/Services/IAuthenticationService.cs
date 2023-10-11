using UsersManager_BAL.Contracts.Models.Authentication;

namespace UsersManager_BAL.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<IAppUser> SignInAsync(Guid userId, bool isPersistent, CancellationToken cancellationToken = default);
        Task<IAppUser> RefreshTokenAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default);
    }
}