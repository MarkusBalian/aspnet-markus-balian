using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IGymClassService
{
    Task<IReadOnlyCollection<GymClass>> GetAvailableClassesAsync();
    Task<bool> BookClassAsync(Guid classId, string userId);
    Task CancelBookingAsync(Guid bookingId, string userId);
    Task<IReadOnlyCollection<Booking>> GetUserBookingsAsync(string userId);
}