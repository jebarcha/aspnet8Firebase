using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Netfirebase.Api.Data;
using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;
using Netfirebase.Api.Models;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Pagination;
using Netfirebase.Api.Vms;

namespace Netfirebase.Api.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IPagedList _pagination;
    private readonly DatabaseContext _context;

    public AuthenticationService(
        HttpClient httpClient, 
        DatabaseContext context, 
        IPagedList pagination
    )
    {
        _httpClient = httpClient;
        _context = context;
        _pagination = pagination;
    }

    public async Task<PagedResults<User>> GetPaginationVersion1(PaginationParams request)
    {
        var query = _context.Users.Include(x => x.Roles)!.ThenInclude(x => x.Permissions);

        return await _pagination.CreatePagedGenericResults<User>(query, 
            request.PageNumber,
            request.PageSize,
            request.OrderBy!,
            request.OrderAsc
        );
    }

    public async Task<PagedResults<UserVm>> GetPaginationVersion2(PaginationParams request)
    {
        var query = _context.Database.SqlQuery<UserVm>(@$"
          SELECT
              usr.""Id"",
              usr.""Email"",
              usr.""FullName"",
              string_agg(rol.""Name"", ',') as ""Role"",
              string_agg(perm.""Name"", ',') as ""Permission""
              FROM ""Users"" as usr
              LEFT JOIN ""UserRole"" as usrol
                ON usr.""Id""=usrol.""UserId""
              LEFT JOIN ""Roles"" as rol
                ON rol.""Id""=usrol.""RoleId""
              LEFT JOIN ""RolePermission"" as rolePermission
                ON rolePermission.""RoleId""=rol.""Id""
              LEFT JOIN ""Permissions"" as perm
                ON perm.""Id""=rolePermission.""PermissionId""
              GROUP BY usr.""Id""  
        ");

        return await _pagination.CreatePagedGenericResults(query, 
            request.PageNumber, 
            request.PageSize, 
            request.OrderBy!, 
            request.OrderAsc);
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
