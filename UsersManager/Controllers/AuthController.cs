using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManager.Commons;
using UsersManager_BAL.Contracts.Services;
using UsersManager_BAL.Models;

namespace UsersManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AuthController(
            IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<GenericResponse> RegisterAsync([FromBody] RegisterModel model, IValidator<RegisterModel> validator)
        {

            var output = await _appUserService.RegisterAsync(model);

            return new GenericResponse(output);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<GenericResponse> LoginAsync([FromBody] LoginModel model)
        {
            var output = await _appUserService.LoginAsync(model);

            return new GenericResponse(output);
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<GenericResponse> RefreshTokenAsync([FromBody] RefreshTokenModel model)
        {
            var output = await _appUserService.RefreshTokenAsync(model);

            return new GenericResponse(output);
        }
    }
}