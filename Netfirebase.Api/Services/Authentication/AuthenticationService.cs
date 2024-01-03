using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Netfirebase.Api.Data;
using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;
using Netfirebase.Api.Models;
using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly DatabaseContext _context;

    public AuthenticationService(HttpClient httpClient, DatabaseContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task<string> LoginAsync(LoginRequestDto request)
    {
        var credentials = new
        {
            request.Email,
            request.Password,
            returnSecureToken = true
        };

        var response = await _httpClient.PostAsJsonAsync("", credentials);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Incorrect Credentials!");
        }


        var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>();

        return authFirebaseObject!.IdToken!;
    }

    public async Task<string> RegisterAsync(UserRegisterRequestDto request)
    {
        var userArgs = new UserRecordArgs
        {
            DisplayName = request.FullName,
            Email = request.Email,
            Password = request.Password
        };

        var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        _context.Users.Add(new User
        {
            Email = request.Email,
            FullName = request.FullName,
            FirebaseId = user.Uid
        });

        await _context.SaveChangesAsync();

        return user.Uid;
    }

}
