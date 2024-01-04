using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Pagination;
using Netfirebase.Api.Vms;

namespace Netfirebase.Api.Services.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(UserRegisterRequestDto request);

    Task<string> LoginAsync(LoginRequestDto request);

    Task<User?> GetUserByEmail(string email);

    Task<PagedResults<User>> GetPaginationVersion1(PaginationParams request);

    Task<PagedResults<UserVm>> GetPaginationVersion2(PaginationParams request);

}
