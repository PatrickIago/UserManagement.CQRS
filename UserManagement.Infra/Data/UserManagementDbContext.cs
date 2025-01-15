using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infra.Data;
public class UserManagementDbContext : DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
        : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
}