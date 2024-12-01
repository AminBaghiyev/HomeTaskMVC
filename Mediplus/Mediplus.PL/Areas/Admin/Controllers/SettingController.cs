using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingController : Controller
{
    readonly ISettingService _settingService;

    public SettingController(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Setting> items = await _settingService.GetAllSettingsAsync();
        if (!items.Any())
        {
            await _settingService.AddSettingAsync(new() { Key = "Phone Number" });
            await _settingService.AddSettingAsync(new() { Key = "E-mail" });
            await _settingService.AddSettingAsync(new() { Key = "Facebook" });
            await _settingService.AddSettingAsync(new() { Key = "Google Plus" });
            await _settingService.AddSettingAsync(new() { Key = "Twitter" });
            await _settingService.AddSettingAsync(new() { Key = "Vimeo" });
            await _settingService.AddSettingAsync(new() { Key = "Pinterest" });
            await _settingService.AddSettingAsync(new() { Key = "About Us" });

            items = await _settingService.GetAllSettingsAsync();
        }

        return View(items);
    }

    public async Task<IActionResult> Update(int Id)
    {
        Setting? setting = await _settingService.GetSettingByIdAsync(Id);
        if (setting == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(setting);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Setting item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }

        await _settingService.UpdateSettingAsync(item.Id, item);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> SetActive(int Id)
    {
        await _settingService.SetActiveSettingAsync(Id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> SetDeactive(int Id)
    {
        await _settingService.SetDeactiveSettingAsync(Id);

        return RedirectToAction(nameof(Index));
    }
}
