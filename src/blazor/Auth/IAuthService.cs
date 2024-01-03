namespace blazor.Auth;

public interface IAuthService
{
    Task<string> Register(RegisterModel registerModel);
    Task<string> Login(LoginModel loginModel);
    Task Logout();
}
