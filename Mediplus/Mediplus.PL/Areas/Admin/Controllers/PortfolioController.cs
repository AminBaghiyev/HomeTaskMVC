using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class PortfolioController : Controller
{
	readonly IPortfolioService _portfolioService;

	public PortfolioController(IPortfolioService portfolioService)
	{
		_portfolioService = portfolioService;
	}

	public async Task<IActionResult> Index()
	{
		IEnumerable<Portfolio> items = await _portfolioService.GetAllPortfoliosAsync();

		return View(items);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Portfolio item)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		await _portfolioService.CreatePortfolioAsync(item);

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Update(int Id)
	{
		Portfolio? portfolio = await _portfolioService.GetPortfolioByIdAsync(Id);
		if (portfolio == null)
		{
			return RedirectToAction(nameof(Index));
		}

		return View(nameof(Create), portfolio);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(Portfolio item)
	{
		if (!ModelState.IsValid)
		{
			return View(nameof(Create), item);
		}

		await _portfolioService.UpdatePortfolioAsync(item.Id, item);

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Remove(int Id)
	{
		await _portfolioService.DeletePortfolioAsync(Id);

		return RedirectToAction(nameof(Index));
	}
}
