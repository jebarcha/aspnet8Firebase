using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;
using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Services.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(UserRegisterRequestDto request);

    Task<string> LoginAsync(LoginRequestDto request);

    Task<User?> GetUserByEmail(string email);
}
