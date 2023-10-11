using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using UsersManager_BAL.Contracts.Models.InputModels;
using UsersManager_BAL.Contracts.Services;
using UsersManager_BAL.Infrastructure.Mapper;
using UsersManager_BAL.Infrastructure.Search.Pagination;
using UsersManager_BAL.Infrastructure.Search.Sorting;
using UsersManager_BAL.Models;
using UsersManager_BAL.Models.OutputModels;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Domain;
using UsersManager_DAL.Domain.Filter;

namespace UsersManager_BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UserOutputModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetUserWithRolesQuery()
                .SingleOrDefaultAsync(u => Equals(u.Id, id), cancellationToken);

            if (user == default)
            {
                throw new KeyNotFoundException(Constants.NotFoundEntityByIdErrorMessage);
            }

            var output = UserMapper.MapAppUserToAppUserOutputModel(user);
            output.SetEnumRoles(user.UserRoles.Select(ur => ur.Role));

            return output;
        }

        public PagedList<UserOutputModel> GetAllUsers(CommonFilterModel<UserFilter> commonFilterModel)
        {
            var users = _userRepository.FilterForList(commonFilterModel.SearchFilter)
                .AsNoTracking()
                .AsSplitQuery()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Sort(commonFilterModel.SortingBy)
                .ToPagedList(commonFilterModel.Filter);

            return users.Select(u =>
            {
                var output = UserMapper.MapAppUserToAppUserOutputModel(u);
                output.SetEnumRoles(u.UserRoles.Select(ur => ur.Role));

                return output;
            });
        }

        public async Task<UserOutputModel> AddUserAsync(IUserAddModel inputModel, CancellationToken cancellationToken = default)
        {
            if (await _userRepository.IsAlreadyExistUserWithCurrentEmail(inputModel.Email))
            {
                throw new ArgumentException(Constants.UserWithCurrentEmailAlreadyExist);
            }

            var user = UserMapper.MapAppUserAddModelToAppUser(inputModel);

            try
            {
                var roles = Array.Empty<Role>();
                if (inputModel.Roles != default && inputModel.Roles.Length > 0)
                {
                    roles = await _unitOfWork.Repository<IRoleRepository>()
                        .GetRolesByEnumName(inputModel.Roles);

                    user.UserRoles = roles.Select(r => new UserRole
                    {
                        RoleId = r.Id
                    }).ToArray();
                }

                await _userRepository.AddAsync(user, cancellationToken);
                await _userRepository.SaveChangesAsync(cancellationToken);

                var output =  UserMapper.MapAppUserToAppUserOutputModel(user);
                output.SetEnumRoles(roles);

                return output;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception.StackTrace);
                throw;
            }
        }

        public async Task<UserOutputModel> UpdateUserAsync(IUserUpdateModel inputModel, CancellationToken cancellationToken = default)
        {
            if (await _userRepository.IsAlreadyExistUserWithCurrentEmail(inputModel.Email, inputModel.Id))
            {
                throw new ArgumentException(Constants.UserWithCurrentEmailAlreadyExist);
            }

            var user = await _userRepository.GetUserWithRolesQuery()
                    .SingleOrDefaultAsync(u => u.Id == inputModel.Id);

            if (user == default)
            {
                throw new ArgumentException(Constants.NotFoundEntityByIdErrorMessage);
            }
            
            var roles = await _unitOfWork.Repository<IRoleRepository>()
                        .GetRolesByEnumName(inputModel.Roles);
            try
            {
                user.Name = inputModel.Name;
                user.Age = inputModel.Age;
                user.Email = inputModel.Email;

                _unitOfWork.Repository<IUserRoleRepository>()
                    .RemoveRangeWithoutSave(user.UserRoles);

                _unitOfWork.Repository<IUserRoleRepository>()
                    .AddRangeWithoutSave(roles.Select(r => new UserRole
                    {
                        RoleId = r.Id,
                        UserId = inputModel.Id
                    }).ToList());

                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();

                var output = UserMapper.MapAppUserToAppUserOutputModel(user);
                output.SetEnumRoles(roles);

                return output;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception.StackTrace);
                throw new InvalidOperationException(Constants.TryToUpdateErrorMessage);
            }
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.Query()
                .AsNoTracking()
                .SingleOrDefaultAsync(u => Equals(u.Id, id), cancellationToken);

            if (user == default)
            {
                throw new ArgumentException(Constants.NotFoundEntityByIdErrorMessage);
            }

            try
            {
                _userRepository.Delete(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception.StackTrace);
                throw;
            }
        }
    }
}