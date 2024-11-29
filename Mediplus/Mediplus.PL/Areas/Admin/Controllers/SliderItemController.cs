using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderItemController : Controller
{
    readonly ISliderItemService _sliderItemService;

    public SliderItemController(ISliderItemService sliderItemService)
    {
        _sliderItemService = sliderItemService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<SliderItem> items = await _sliderItemService.GetAllSliderItemsAsync();

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

        await _sliderItemService.CreateSliderItemAsync(item);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        SliderItem? sliderItem = await _sliderItemService.GetSliderItemByIdAsync(Id);
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

        await _sliderItemService.UpdateSliderItemAsync(item.Id, item);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int Id)
    {
        await _sliderItemService.DeleteSliderItemAsync(Id);

        return RedirectToAction(nameof(Index));
    }
}
