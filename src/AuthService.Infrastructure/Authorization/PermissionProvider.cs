using AuthService.Application.Abstractions.Data;
using AuthService.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.Authorization;

public sealed class PermissionProvider(IApplicationDbContext context)
{ 
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        //Using claims from JWT token to get userId


        ICollection<Role> roles =  await context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .ToArrayAsync();

        var permissionsSet = roles
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();

        return permissionsSet;
    }
}
