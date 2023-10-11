namespace UsersManager_BAL.Contracts.Models.OutputModels
{
    public interface IUserOutputModel : IUserModel
    {
        void SetEnumRoles(IEnumerable<UsersManager_DAL.Domain.Role> roles);
    }
}