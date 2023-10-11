using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Repositories
{
    public class RefreshTokenRepository : EfRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}