using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Repositories
{
    public class UserRoleRepository : EfRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}