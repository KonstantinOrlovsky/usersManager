using Microsoft.EntityFrameworkCore;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Contracts.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role[]> GetRolesByEnumName(IEnumerable<Domain.Enums.Role> roleNames);
    }
}