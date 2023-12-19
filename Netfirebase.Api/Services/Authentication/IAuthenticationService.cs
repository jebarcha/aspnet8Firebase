using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;

namespace Netfirebase.Api.Services.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(UserRegisterRequestDto request);

    Task<string> LoginAsync(LoginRequestDto request);
}
