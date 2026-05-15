using CoreFitnessClub.Domain.Entities;

namespace Webapp.Models.MyPage;

public class MyPageViewModel
{
    public Membership? Membership { get; set; }
    public IReadOnlyCollection<Booking> Bookings { get; set; } = [];
}