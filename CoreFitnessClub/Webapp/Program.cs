using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Data;
using CoreFitnessClub.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IGymClassService, GymClassService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.GymClasses.Any())
    {
        db.GymClasses.AddRange(
            new GymClass { Name = "Personal Training - Strength", Category = "Personal Training", Instructor = "Alex", StartTimeUtc = DateTime.UtcNow.Date.AddDays(1).AddHours(8), Capacity = 1 },
            new GymClass { Name = "Group Training - HIIT", Category = "Group Training", Instructor = "Mia", StartTimeUtc = DateTime.UtcNow.Date.AddDays(1).AddHours(17), Capacity = 20 },
            new GymClass { Name = "Padel Fundamentals", Category = "Padel", Instructor = "Jonas", StartTimeUtc = DateTime.UtcNow.Date.AddDays(2).AddHours(18), Capacity = 8 }
        );
        db.SaveChanges();
    }
}

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();