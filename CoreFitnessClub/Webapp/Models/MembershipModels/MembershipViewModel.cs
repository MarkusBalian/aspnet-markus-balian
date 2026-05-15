using System.ComponentModel.DataAnnotations;

namespace Webapp.Models.MembershipModels;

public class MembershipViewModel
{
    [Required]
    public string PlanName { get; set; } = MembershipPlans.Standard;

    public IReadOnlyList<string> AvailablePlans { get; set; } = MembershipPlans.Allowed;
}