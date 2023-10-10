using UsersManager_BAL.Contracts.Models.OutputModels;

namespace UsersManager_BAL.Models
{
    public class UserRoleModel : IUserRoleModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}