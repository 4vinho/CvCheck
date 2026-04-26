using api.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Infra.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<EmailConfirmationCode> EmailConfirmationCodes => Set<EmailConfirmationCode>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EmailConfirmationCode>(entity =>
        {
            entity.HasKey(code => code.Id);
            entity.Property(code => code.Code).HasMaxLength(6);
            entity.HasIndex(code => new { code.UserId, code.Code });
            entity.HasOne(code => code.User)
                .WithMany(user => user.EmailConfirmationCodes)
                .HasForeignKey(code => code.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
