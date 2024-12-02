using Inance.Enums;
using Inance.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inance.Controllers;

public class HomeController : Controller
{
    readonly RoleManager<IdentityRole> _roleManager;
    readonly UserManager<AppUser> _userManager;

    public HomeController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new () { Name = role.ToString() });
            }
        }

        var result = await _userManager.FindByNameAsync("admin");
        if (result is null)
        {
            AppUser admin = new()
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                Email = "admin@inance.com"
            };

            await _userManager.CreateAsync(admin, "admin123");
            await _userManager.AddToRoleAsync(admin, "Admin");
        }

        return View();
    }
}
