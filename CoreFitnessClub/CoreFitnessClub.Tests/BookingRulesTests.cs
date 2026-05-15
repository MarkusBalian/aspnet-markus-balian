using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Data;
using CoreFitnessClub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Tests;

public class BookingRulesTests
{
    [Fact]
    public async Task BookClassAsync_PreventsDuplicateBookings()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var db = new ApplicationDbContext(options);
        var gymClass = new GymClass { Name = "Spinning", StartTimeUtc = DateTime.UtcNow.AddDays(1) };
        db.GymClasses.Add(gymClass);
        await db.SaveChangesAsync();

        var service = new GymClassService(db);
        var first = await service.BookClassAsync(gymClass.Id, "user-1");
        var second = await service.BookClassAsync(gymClass.Id, "user-1");

        Assert.True(first);
        Assert.False(second);
    }
}