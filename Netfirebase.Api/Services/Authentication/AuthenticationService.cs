using FirebaseAdmin.Auth;
using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;
using Netfirebase.Api.Models;

namespace Netfirebase.Api.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<string> LoginAsync(LoginRequestDto request)
    {
        var credentials = new
        {
            request.Email,
            request.Password,
            returnSecureToken = true
        };

        var response = await _httpClient.PostAsJsonAsync("", request);

        var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>();

        return authFirebaseObject!.IdToken!;
    }

    public async Task<string> RegisterAsync(UserRegisterRequestDto request)
    {
        var userArgs = new UserRecordArgs
        {
            Email = request.Email,
            Password = request.Password
        };

        var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);
        return user.Uid;
    }

}
