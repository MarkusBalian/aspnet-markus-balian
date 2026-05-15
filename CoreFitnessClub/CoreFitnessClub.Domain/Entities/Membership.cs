using CoreFitnessClub.Domain.Enums;

namespace CoreFitnessClub.Domain.Entities;

public class Membership
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;
    public MembershipStatus Status { get; set; } = MembershipStatus.Pending;
}