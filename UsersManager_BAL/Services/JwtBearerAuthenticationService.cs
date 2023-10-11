using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UsersManager_BAL.Contracts.Models.Authentication;
using UsersManager_BAL.Contracts.Services;
using UsersManager_BAL.Models.Authentication;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Domain;

namespace UsersManager_BAL.Services
{
    public class JwtBearerAuthenticationService : IAuthenticationService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IConfiguration _configuration;
        private int _tokenLifeTimeInMinutes;
        private int _refreshTokenLifeTimeInDays;

        private const string TokenLifeTimeInMinutesConfigKey = "Authorization:TokenLifeTimeInMinutes";
        private const string RefreshTokenLifeTimeInDaysConfigKey = "Authorization:RefreshTokenLifeTimeInDays";

        public JwtBearerAuthenticationService(
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;

            ReadConfiguration();
        }

        public async Task<IAppUser> SignInAsync(Guid userId, bool isPersistent, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.TokenEncryptKey);
            
            var userClaimData = new UserClaimData()
            {
                UserId = userId
            };
            var userIdentity = new ClaimsIdentity(
                new[]
                {
                    new Claim(
                        ClaimTypes.UserData, 
                        JsonConvert.SerializeObject(userClaimData),
                        Constants.AuthenticationScheme)
                });

            var expires = DateTime.UtcNow.AddMinutes(_tokenLifeTimeInMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = userIdentity,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = await GenerateRefreshTokenAsync(userId, _refreshTokenRepository, cancellationToken);

            var appUser = new AppUser(userId, tokenHandler.WriteToken(token), refreshToken.Value);

            return appUser;
        }

        public async Task<IAppUser> RefreshTokenAsync(Guid userId, string refreshTokenValue, CancellationToken cancellationToken = default)
        {
            var refreshToken = await _refreshTokenRepository.Query()
                .FirstOrDefaultAsync(rt =>
                    Equals(rt.UserId, userId) 
                    && Equals(rt.Value, refreshTokenValue)
                    && rt.IsUsed == false
                    && rt.ExpiredDate > DateTime.Now, cancellationToken);

            if (refreshToken == default)
            {
                throw new ArgumentException(Constants.InvalidRefreshToken);
            }

            refreshToken.IsUsed = true;
            _refreshTokenRepository.Update(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return await SignInAsync(userId, true, cancellationToken);
        }

        private async Task<RefreshToken> GenerateRefreshTokenAsync(
            Guid userId, 
            IRepository<RefreshToken> refreshTokenRepository, 
            CancellationToken cancellationToken = default)
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            var refreshToken = new RefreshToken()
            {
                UserId = userId,
                Value = Convert.ToBase64String(randomNumber),
                CreatedDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddDays(_refreshTokenLifeTimeInDays)
            };

            await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            await refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }

        private void ReadConfiguration()
        {
            var tokenLifeTimeInMinutesConfigValue = _configuration[TokenLifeTimeInMinutesConfigKey];
            var refreshTokenLifeTimeInDaysConfigValue = _configuration[RefreshTokenLifeTimeInDaysConfigKey];
            _tokenLifeTimeInMinutes = Convert.ToInt32(tokenLifeTimeInMinutesConfigValue);
            _refreshTokenLifeTimeInDays = Convert.ToInt32(refreshTokenLifeTimeInDaysConfigValue);
        }
    }
}