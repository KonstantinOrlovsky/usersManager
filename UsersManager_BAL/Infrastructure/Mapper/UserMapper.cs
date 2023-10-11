using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_BAL.Models.Authentication.Security;
using UsersManager_BAL.Models.OutputModels;
using UsersManager_DAL.Domain;

namespace UsersManager_BAL.Infrastructure.Mapper
{
    public static class UserMapper
    {
        public static UserOutputModel MapAppUserToAppUserOutputModel(User appUser)
        {
            return new UserOutputModel
            {
                Id = appUser.Id,
                Age = appUser.Age,
                Email = appUser.Email,
                Name = appUser.Name,
            };
        }

        public static User MapAppUserAddModelToAppUser(IUserAddModel appUser)
        {
            var p = Password.Create(appUser.Password);

            return new User
            {
                Id = Guid.NewGuid(),
                Age = appUser.Age,
                Email = appUser.Email,
                Name = appUser.Name,
                PasswordHash = p.Hash
            };
        }
    }
}