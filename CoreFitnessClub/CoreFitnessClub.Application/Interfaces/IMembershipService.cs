using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IMembershipService
{
    Task<Membership?> GetMembershipAsync(string userId);
    Task<Membership> CreateOrUpdateMembershipAsync(string userId, string planName);
    Task DeleteMembershipAsync(string userId);
}