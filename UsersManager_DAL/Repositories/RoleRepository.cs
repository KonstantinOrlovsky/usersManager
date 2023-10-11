using Microsoft.EntityFrameworkCore;
using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Repositories
{
    public class RoleRepository : EfRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role[]> GetRolesByEnumName(IEnumerable<Domain.Enums.Role> roleNames)
        {
            return await GetAll()
                .Where(r => roleNames.Select(r => r.ToString()).Contains(r.Name))
                .ToArrayAsync();
        }
    }
}