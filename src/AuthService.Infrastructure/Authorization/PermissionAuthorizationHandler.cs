using AuthService.Infrastructure.Authentication;
using AuthService.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure.Authentication;

namespace SharedKernel.Infrastructure.Authorization;

public sealed class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory )
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // TODO: You definitely want to reject unauthenticated users here.
        //HashSet<string> permissions = [.. context
        //    .User
        //    .Claims.Where(c => c.Type == CustomClaims.Permissions)
        //    .Select(c => c.Value)];

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        PermissionProvider permissionProvider = scope.ServiceProvider.GetRequiredService<PermissionProvider>();

        Guid userId = context.User.GetUserId();

        HashSet<string> permissions = await permissionProvider.GetForUserIdAsync(userId);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
