using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_BAL.Infrastructure.Search.Pagination;
using UsersManager_BAL.Models;
using UsersManager_BAL.Models.OutputModels;
using UsersManager_DAL.Domain.Filter;

namespace UsersManager_BAL.Contracts.Services
{
    public interface IUserService
    {
        public Task<UserOutputModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public PagedList<UserOutputModel> GetAllUsers(CommonFilterModel<UserFilter> commonFilterModel);
        public Task<UserOutputModel> AddUserAsync(IUserAddModel user, CancellationToken cancellationToken = default);
        public Task<UserOutputModel> UpdateUserAsync(IUserUpdateModel user, CancellationToken cancellationToken = default);
        public Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
    }
}