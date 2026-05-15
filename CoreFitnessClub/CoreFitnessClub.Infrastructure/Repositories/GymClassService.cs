using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Repositories;

public class GymClassService(ApplicationDbContext dbContext) : IGymClassService
{
    public async Task<IReadOnlyCollection<GymClass>> GetAvailableClassesAsync() =>
        await dbContext.GymClasses.AsNoTracking().OrderBy(c => c.StartTimeUtc).ToListAsync();

    public async Task<bool> BookClassAsync(Guid classId, string userId)
    {
        var exists = await dbContext.Bookings.AnyAsync(b => b.UserId == userId && b.GymClassId == classId);
        if (exists) return false;

        dbContext.Bookings.Add(new Booking { GymClassId = classId, UserId = userId });
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task CancelBookingAsync(Guid bookingId, string userId)
    {
        var booking = await dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);
        if (booking is null) return;
        dbContext.Bookings.Remove(booking);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Booking>> GetUserBookingsAsync(string userId) =>
        await dbContext.Bookings.Include(b => b.GymClass).Where(b => b.UserId == userId).AsNoTracking().ToListAsync();
}