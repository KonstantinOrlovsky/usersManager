using UsersManager_BAL.Contracts.Models.OutputModels;
using UsersManager_DAL.Domain.Enums;

namespace UsersManager_BAL.Models.OutputModels
{
    public class UserOutputModel : UserModel, IUserOutputModel
    {
        public void SetEnumRoles(IEnumerable<UsersManager_DAL.Domain.Role> roles)
        {
            Roles = roles.Select(r => Enum.Parse<Role>(r.Name))
                    .ToArray() ?? Array.Empty<Role>();
        }
    }
}