using CoreFitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Webapp.Controllers;

[Authorize]
public class ClassesController(IGymClassService gymClassService) : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index() => View(await gymClassService.GetAvailableClassesAsync());

    [HttpPost]
    public async Task<IActionResult> Book(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var booked = await gymClassService.BookClassAsync(id, userId);
        TempData["Message"] = booked ? "Class booked." : "You are already booked for this class.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await gymClassService.CancelBookingAsync(id, userId);
        return RedirectToAction("Index", "MyPage");
    }
}