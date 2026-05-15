namespace Webapp.Models.MembershipModels;

public static class MembershipPlans
{
    public const string Standard = "Standard";
    public const string Premium = "Premium";

    public static readonly IReadOnlyList<string> Allowed = [Standard, Premium];
}