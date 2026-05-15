using CoreFitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Webapp.Models.MyPage;

namespace Webapp.Controllers;

[Authorize]
public class MyPageController(IMembershipService membershipService, IGymClassService gymClassService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var model = new MyPageViewModel
        {
            Membership = await membershipService.GetMembershipAsync(userId),
            Bookings = await gymClassService.GetUserBookingsAsync(userId)
        };

        return View(model);
    }
}