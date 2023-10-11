using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_DAL.Domain.Enums;

namespace UsersManager_BAL.Models.InputModels
{
    public class UserAddModel : IUserAddModel
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public Role[]? Roles { get; set; }
    }
}