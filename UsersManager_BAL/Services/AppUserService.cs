using Microsoft.EntityFrameworkCore;
using UsersManager_BAL.Contracts.Models.Authentication;
using UsersManager_BAL.Contracts.Services;
using UsersManager_BAL.Models;
using UsersManager_BAL.Models.Authentication.Security;
using UsersManager_DAL.Contracts.Repositories;

namespace UsersManager_BAL.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public AppUserService(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<IAppUser> LoginAsync(LoginModel inputModel, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => Equals(u.Name, inputModel.Login) && Equals(u.PasswordHash, Password.CalculateHash(inputModel.Password)), 
                    cancellationToken);

            if (user == default)
            {
                throw new ArgumentException(Constants.LoginOrPasswordIsWrong);
            }

            var appUser = await _authenticationService.SignInAsync(user.Id, true, cancellationToken);

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return appUser;
        }

        public async Task<IAppUser> RefreshTokenAsync(RefreshTokenModel inputModel, CancellationToken cancellationToken = default)
        {
            var userId = Guid.Parse(inputModel.UserId);
            var user = await _userRepository.Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => Equals(u.Id, userId), cancellationToken);

            if (user == default)
            {
                throw new ArgumentException(Constants.NotFoundEntityByIdErrorMessage);
            }

            return await _authenticationService.RefreshTokenAsync(userId, inputModel.RefreshToken, cancellationToken);
        }
    }
}