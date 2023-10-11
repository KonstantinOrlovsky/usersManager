namespace UsersManager_BAL.Contracts.Models.OutputModels
{
    public interface IUserRoleModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}