using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlterForm;

public class AlterDB : IdentityDbContext<AlterUser, IdentityRole<Guid>, Guid>
{
    public AlterDB(DbContextOptions<AlterDB> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

}