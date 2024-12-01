using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : Controller
{
	readonly IBaseService<Blog> _blogService;

	public BlogController(IBaseService<Blog> blogService)
	{
		_blogService = blogService;
	}

	public async Task<IActionResult> Index()
	{
		IEnumerable<Blog> items = await _blogService.GetAllAsync();

		return View(items);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Blog item)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		await _blogService.CreateAsync(item);

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Update(int Id)
	{
		Blog? portfolio = await _blogService.GetByIdAsync(Id);
		if (portfolio == null)
		{
			return RedirectToAction(nameof(Index));
		}

		return View(nameof(Create), portfolio);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(Blog item)
	{
		if (!ModelState.IsValid)
		{
			return View(nameof(Create), item);
		}

		await _blogService.UpdateAsync(item.Id, item);

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Remove(int Id)
	{
		await _blogService.DeleteAsync(Id);

		return RedirectToAction(nameof(Index));
	}
}
