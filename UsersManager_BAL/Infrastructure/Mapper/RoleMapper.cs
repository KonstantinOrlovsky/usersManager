using UsersManager_BAL.Models;
using UsersManager_DAL.Domain;

namespace UsersManager_BAL.Infrastructure.Mapper
{
    public static class RoleMapper
    {
        public static UserRoleModel MapAppUserToAppUserOutputModel(Role userRole)
        {
            return new UserRoleModel
            {
                Id = userRole.Id,
                Name = userRole.Name,
            };
        }
    }
}