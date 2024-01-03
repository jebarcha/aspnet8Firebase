using Microsoft.AspNetCore.Authorization;
using Netfirebase.Api.Services.Permissions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Netfirebase.Api.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier
        )?.Value;

        if (userId is null)
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionsAsync(userId!);
        
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

    }
}
