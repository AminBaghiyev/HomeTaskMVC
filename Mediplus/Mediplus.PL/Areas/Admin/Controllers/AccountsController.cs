using Mediplus.DAL.Models;
using Mediplus.BL.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountsController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Dashboard");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterUserDto registerUser)
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Dashboard");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser user = new()
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Email = registerUser.Email,
            UserName = registerUser.UserName
        };

        IdentityResult res = await _userManager.CreateAsync(user, registerUser.Password);

        if (!res.Succeeded)
        {
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View();
        }

        return RedirectToAction(nameof(Login));
    }

    public IActionResult Login()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Dashboard");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginUserDto loginUser)
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Dashboard");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser? user = await _userManager.FindByNameAsync(loginUser.UserName);

        if(user == null)
        {
            ModelState.AddModelError("CustomError", "Fields are wrong!");
            return View();
        }

        var res = await _signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, true);
        if (!res.Succeeded)
        {
            ModelState.AddModelError("CustomError", "Fields are wrong!");
            return View();
        }

        return RedirectToAction(nameof(Index), "Dashboard");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
}
