using UsersManager_DAL.Domain.Enums;

namespace UsersManager_BAL.Contracts.Models
{
    public interface IUserModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        int Age { get; set; }
        string Email { get; set; }
        Role[]? Roles { get; set; }
    }
}