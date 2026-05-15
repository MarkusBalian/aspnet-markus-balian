using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Domain.Enums;
using CoreFitnessClub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Repositories;

public class MembershipService(ApplicationDbContext dbContext) : IMembershipService
{
    public Task<Membership?> GetMembershipAsync(string userId) => dbContext.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.UserId == userId);

    public async Task<Membership> CreateOrUpdateMembershipAsync(string userId, string planName)
    {
        if (planName is not ("Standard" or "Premium"))
            throw new ArgumentException("Plan must be Standard or Premium.", nameof(planName));
        var membership = await dbContext.Memberships.FirstOrDefaultAsync(m => m.UserId == userId);
        if (membership is null)
        {
            membership = new Membership { UserId = userId, PlanName = planName, Status = MembershipStatus.Active };
            dbContext.Memberships.Add(membership);
        }
        else
        {
            membership.PlanName = planName;
            membership.Status = MembershipStatus.Active;
        }

        await dbContext.SaveChangesAsync();
        return membership;
    }

    public async Task DeleteMembershipAsync(string userId)
    {
        var membership = await dbContext.Memberships.FirstOrDefaultAsync(m => m.UserId == userId);
        if (membership is null) return;
        dbContext.Memberships.Remove(membership);
        await dbContext.SaveChangesAsync();
    }
}