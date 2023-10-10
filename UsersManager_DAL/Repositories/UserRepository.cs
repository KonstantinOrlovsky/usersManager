using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Domain;
using UsersManager_DAL.Domain.Filter;

namespace UsersManager_DAL.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<User> FilterForList(UserFilter filter, Expression<Func<User, bool>>? predicate = default)
        {
            var query = GetAll();

            if (filter.Name != string.Empty)
            {
                query = query.Where(u => u.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.Age.HasValue)
            {
                query = query.Where(u => u.Age == filter.Age);
            }

            if (filter.Email != string.Empty)
            {
                query = query.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            if (filter.Roles != default && filter.Roles.Count > 0)
            {
                var roleNames = filter.Roles.Select(r => r.ToString());
                query = query = query.Where(u => u.UserRoles.Any(r => roleNames.Contains(r.Role.Name)));
            }

            return query;
        }

        public IQueryable<User> GetUserWithRolesQuery()
        {
            return Query()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);
        }

        public async Task<bool> IsAlreadyExistUserWithCurrentEmail(string email, Guid? userId = null)
        {
            return await GetAll()
                .AnyAsync(u =>string.Equals(u.Email, email) && u.Id != userId);
        }
    }
}