using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UsersManager.Commons;
using UsersManager_BAL.Contracts.Services;
using UsersManager_BAL.Models;
using UsersManager_BAL.Models.InputModels;
using UsersManager_DAL.Domain.Filter;

namespace UsersManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _appUserService;
        private IValidator<AppUserAddModel> _addModelValidator;
        private IValidator<AppUserUpdateModel> _updateModelValidator;

        public UserController(
            IUserService appUserService, 
            IValidator<AppUserAddModel> addModelValidator,
            IValidator<AppUserUpdateModel> updateModelValidator)
        {
            _appUserService = appUserService;
            _addModelValidator = addModelValidator;
            _updateModelValidator = updateModelValidator;
        }

        [Route("{id}"), HttpGet]
        public async Task<GenericResponse> GetUser([FromRoute] Guid id)
        {
            var output = await _appUserService.GetUserByIdAsync(id);

            return new GenericResponse(output);
        }

        [HttpGet]
        public GenericResponse GetUsers(
            [FromQuery] CommonFilterModel<UserFilter> commonFilterModel)
        {
            var output = _appUserService.GetAllUsers(commonFilterModel);

            return new GenericResponse(output);
        }

        [HttpPost]
        public async Task<GenericResponse> AddUser([FromBody] AppUserAddModel model)
        {
            _addModelValidator.ValidateAndThrow(model);

            var output = await _appUserService.AddUserAsync(model);

            return new GenericResponse(output);
        }

        [HttpPut]
        public async Task<GenericResponse> UpdateUser([FromBody] AppUserUpdateModel model)
        {
            _updateModelValidator.ValidateAndThrow(model);

            var output = await _appUserService.UpdateUserAsync(model);

            return new GenericResponse(output);
        }

        [Route("{id}"), HttpDelete]
        public async Task<GenericResponse> DeleteUser([FromRoute] Guid id)
        {
            await _appUserService.DeleteUserAsync(id);

            return new GenericResponse();
        }
    }
}