using CoreFitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<GymClass> GymClasses => Set<GymClass>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Booking>().HasIndex(b => new { b.UserId, b.GymClassId }).IsUnique();
        builder.Entity<Membership>().HasIndex(m => m.UserId).IsUnique();
    }
}