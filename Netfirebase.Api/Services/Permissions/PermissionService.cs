
using Netfirebase.Api.Data;
using Netfirebase.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Netfirebase.Api.Services.Permissions;

public class PermissionService : IPermissionService
{
    private readonly DatabaseContext _context;

    public PermissionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(string userId)
    {
        ICollection<Role>[] roles = await _context.Set<User>()
            .Include(x => x.Roles!)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.FirebaseId == userId)
            .Select(x => x.Roles!)
            .ToArrayAsync();

        return roles.SelectMany(x => x)
            .SelectMany(x => x.Permissions!)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
