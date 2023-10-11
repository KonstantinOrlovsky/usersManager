using UsersManager_BAL.Contracts.Models.Authentication;

namespace UsersManager_BAL.Models.Authentication
{
    public class UserClaimData : IUserClaimData
    {
        public Guid UserId { get; set; }
    }
}