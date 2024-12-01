using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderItemController : Controller
{
    readonly IBaseService<SliderItem> _sliderItemService;

    public SliderItemController(IBaseService<SliderItem> sliderItemService)
    {
		_sliderItemService = sliderItemService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<SliderItem> items = await _sliderItemService.GetAllAsync();

        return View(items);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SliderItem item)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        await _sliderItemService.CreateAsync(item);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        SliderItem? sliderItem = await _sliderItemService.GetByIdAsync(Id);
        if(sliderItem == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(nameof(Create), sliderItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(SliderItem item)
    {
        if(!ModelState.IsValid)
        {
            return View(nameof(Create), item);
        }

        await _sliderItemService.UpdateAsync(item.Id, item);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int Id)
    {
        await _sliderItemService.DeleteAsync(Id);

        return RedirectToAction(nameof(Index));
    }
}
