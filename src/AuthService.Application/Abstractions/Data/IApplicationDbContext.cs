using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
