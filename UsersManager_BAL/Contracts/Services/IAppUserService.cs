using UsersManager_BAL.Contracts.Models.Authentication;
using UsersManager_BAL.Models;

namespace UsersManager_BAL.Contracts.Services
{
    public interface IAppUserService
    {
        Task<IAppUser> LoginAsync(LoginModel inputModel, CancellationToken cancellationToken = default);
        Task<IAppUser> RefreshTokenAsync(RefreshTokenModel inputModel, CancellationToken cancellationToken = default);
    }
}