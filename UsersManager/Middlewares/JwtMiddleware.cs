using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using UsersManager_BAL;

namespace UsersManager.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader != default && authHeader.StartsWith("Bearer"))
            {
                string token = authHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    ValidateAndThrow(token, out JwtSecurityToken validatedToken);
                }
                catch
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }

            await _next(context);
        }

        public bool ValidateAndThrow(string token, out JwtSecurityToken jwt)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.TokenEncryptKey)),
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                jwt = (JwtSecurityToken)validatedToken;
               
                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                _logger.LogError(ex.Message);

                jwt = default;
                return default;
            }
        }
    }
}
