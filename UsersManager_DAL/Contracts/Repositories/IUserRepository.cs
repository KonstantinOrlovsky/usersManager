using System.Linq.Expressions;
using UsersManager_DAL.Domain;
using UsersManager_DAL.Domain.Filter;

namespace UsersManager_DAL.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> FilterForList(UserFilter filter, Expression<Func<User, bool>>? predicate = default);
        IQueryable<User> GetUserWithRolesQuery();
        Task<bool> IsAlreadyExistUserWithCurrentEmail(string email, Guid? userId = default);
    }
}