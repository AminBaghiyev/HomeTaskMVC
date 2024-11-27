using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.DTOs.UserDTOs;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.Controllers;

public class AccountsController : Controller
{
    private readonly AppDBContext _db;

    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    public AccountsController(AppDBContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CreateUserDto formUser)
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

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

    public IActionResult Login()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginUserDto formUser)
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser? user = await _db.Users.FirstOrDefaultAsync(
            u => (u.UserName == formUser.UserNameOrEmail || u.Email == formUser.UserNameOrEmail)
        );

        if (user == null)
        {
            ModelState.AddModelError("Password", "Fields are invalid!");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(user, formUser.Password, formUser.RememberMe, true);
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("Password", "You are locked out. Please wait!");
            return View();
        }
        if (!result.Succeeded)
        {
            ModelState.AddModelError("Password", "Fields are invalid!");
            return View();
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Index), "Home");
    }

    [ActionName("User")]
    public async Task<IActionResult> UserInfo()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated || User.Identity.Name is null)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        InfoUserDto? user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user == null) return RedirectToAction(nameof(Index), "Home");

        return View(user);
    }
}
