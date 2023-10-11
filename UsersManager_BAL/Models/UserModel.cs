using UsersManager_BAL.Contracts.Models;
using UsersManager_DAL.Domain.Enums;

namespace UsersManager_BAL.Models
{
    public class UserModel : IUserModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public Role[]? Roles { get; set; }
    }
}