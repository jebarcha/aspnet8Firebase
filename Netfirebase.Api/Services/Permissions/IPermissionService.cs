namespace Netfirebase.Api.Services.Permissions;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(string userId);
}
