using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DTOs.UserDTOs;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.Controllers;

public class AccountsController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public AccountsController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDto formUser)
    {
        if (!ModelState.IsValid)
        {
            return View(formUser);
        }

        AppUser user = new()
        {
            FirstName = formUser.FirstName,
            LastName = formUser.LastName,
            UserName = formUser.Username,
            Email = formUser.Email
        };

        IdentityResult res = await _userManager.CreateAsync(user, formUser.Password);
        if (!res.Succeeded)
        {
            foreach (var e in res.Errors)
            {
                if (e.Code.Contains("Password"))
                {
                    ModelState.AddModelError("Password", e.Description);
                }
            }

            return View(formUser);
        }

        return RedirectToAction("Index", "Home");
    }
}
