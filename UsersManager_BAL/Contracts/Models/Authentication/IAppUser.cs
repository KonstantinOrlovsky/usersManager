namespace UsersManager_BAL.Contracts.Models.Authentication
{
    public interface IAppUser
    {
        Guid Id { get; }
        string? Token { get; }
        string? RefreshToken { get; }
    }
}