using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_BAL.Models;
using UsersManager_BAL.Models.Authentication.Security;
using UsersManager_BAL.Models.InputModels;
using UsersManager_BAL.Models.OutputModels;
using UsersManager_DAL.Domain;
using RoleEnum = UsersManager_DAL.Domain.Enums.Role;

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

        public static UserAddModel MapRegisterModelToUserAddModel(RegisterModel inputModel)
        {
            return new UserAddModel
            {
                Age = default,
                Email = inputModel.Email,
                Name = inputModel.Name,
                Password = inputModel.Password,
                Roles = new RoleEnum[] { RoleEnum.SuperAdmin }
            };
        }
    }
}