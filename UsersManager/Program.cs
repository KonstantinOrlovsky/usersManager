using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using UsersManager.Middlewares;
using UsersManager_BAL;
using UsersManager_BAL.Contracts.Services;
using UsersManager_BAL.Infrastructure.FluentValidation;
using UsersManager_BAL.Models.InputModels;
using UsersManager_BAL.Services;
using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;
using UsersManager_DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<AppDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddControllers().AddJsonOptions(options =>
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<IAppUserService, AppUserService>();
builder.Services.AddTransient<IAuthenticationService, JwtBearerAuthenticationService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IValidator<UserAddModel>, AddModelValidator>();
builder.Services.AddScoped<IValidator<UserUpdateModel>, AppUserUpdateModelValidator>();

#region Swagger Configuration
builder.Services.AddSwaggerGen(swagger => {
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Users manager",
        Version = "v1"
    });

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});
#endregion

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = Constants.AuthenticationScheme;
}).AddJwtBearer(Constants.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.TokenEncryptKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users manager API V1");
});

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
