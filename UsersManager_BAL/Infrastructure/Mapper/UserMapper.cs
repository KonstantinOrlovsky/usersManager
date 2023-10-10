using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_BAL.Models.OutputModels;
using UsersManager_DAL.Domain;

namespace UsersManager_BAL.Infrastructure.Mapper
{
    public static class UserMapper
    {
        public static AppUserOutputModel MapAppUserToAppUserOutputModel(User appUser)
        {
            return new AppUserOutputModel
            {
                Id = appUser.Id,
                Age = appUser.Age,
                Email = appUser.Email,
                Name = appUser.Name,
                Password = appUser.PasswordHash
            };
        }

        public static User MapAppUserAddModelToAppUser(IUserAddModel appUser)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Age = appUser.Age,
                Email = appUser.Email,
                Name = appUser.Name,
                PasswordHash = appUser.Password
            };
        }
    }
}