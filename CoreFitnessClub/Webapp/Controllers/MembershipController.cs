using CoreFitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Webapp.Models.MembershipModels;

namespace Webapp.Controllers;

[Authorize]
public class MembershipController(IMembershipService membershipService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var membership = await membershipService.GetMembershipAsync(userId);

        var model = new MembershipViewModel
        {
            PlanName = membership?.PlanName ?? MembershipPlans.Standard,
            AvailablePlans = MembershipPlans.Allowed
        };

        ViewData["CurrentStatus"] = membership?.Status.ToString() ?? "No membership";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Save(MembershipViewModel model)
    {
        if (!MembershipPlans.Allowed.Contains(model.PlanName))
            ModelState.AddModelError(nameof(model.PlanName), "Invalid membership plan.");

        if (!ModelState.IsValid)
        {
            model.AvailablePlans = MembershipPlans.Allowed;
            return View("Index", model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await membershipService.CreateOrUpdateMembershipAsync(userId, model.PlanName);
        return RedirectToAction(nameof(Index));
    }
}