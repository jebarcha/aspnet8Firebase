using Microsoft.AspNetCore.Authorization;
using Netfirebase.Api.Models.Enums;

namespace Netfirebase.Api.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionEnum permission): base(policy: permission.ToString())
    {
    }
}
