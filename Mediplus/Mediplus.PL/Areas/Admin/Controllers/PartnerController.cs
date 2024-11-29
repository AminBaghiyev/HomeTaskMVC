using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class PartnerController : Controller
{
	readonly IPartnerService _partnerService;

	public PartnerController(IPartnerService partnerService)
	{
		_partnerService = partnerService;
	}

	public async Task<IActionResult> Index()
	{
		IEnumerable<Partner> items = await _partnerService.GetAllPartnersAsync();

		return View(items);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Partner item)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		await _partnerService.CreatePartnerAsync(item);

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Update(int Id)
	{
		Partner? portfolio = await _partnerService.GetPartnerByIdAsync(Id);
		if (portfolio == null)
		{
			return RedirectToAction(nameof(Index));
		}

		return View(nameof(Create), portfolio);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(Partner item)
	{
		if (!ModelState.IsValid)
		{
			return View(nameof(Create), item);
		}

		await _partnerService.UpdatePartnerAsync(item.Id, item);

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Remove(int Id)
	{
		await _partnerService.DeletePartnerAsync(Id);

		return RedirectToAction(nameof(Index));
	}
}
