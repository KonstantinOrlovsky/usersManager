using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_BAL.Infrastructure.Search.Pagination;
using UsersManager_BAL.Models;
using UsersManager_BAL.Models.OutputModels;
using UsersManager_DAL.Domain.Filter;

namespace UsersManager_BAL.Contracts.Services
{
    public interface IUserService
    {
        public Task<AppUserOutputModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public PagedList<AppUserOutputModel> GetAllUsers(CommonFilterModel<UserFilter> commonFilterModel);
        public Task<AppUserOutputModel> AddUserAsync(IUserAddModel appUser, CancellationToken cancellationToken = default);
        public Task<AppUserOutputModel> UpdateUserAsync(IUserUpdateModel appUser, CancellationToken cancellationToken = default);
        public Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
    }
}