using Mediplus.BL.Services.Concretes;
using Mediplus.PL.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Controllers;

public class HomeController : Controller
{
    readonly ISliderItemService _sliderItemService;
    readonly IPortfolioService _portfolioService;
    readonly IPartnerService _partnerService;
    readonly IBlogService _blogService;

	public HomeController(ISliderItemService sliderItemService, IPortfolioService portfolioService, IPartnerService partnerService, IBlogService blogService)
    {
        _sliderItemService = sliderItemService;
        _portfolioService = portfolioService;
        _partnerService = partnerService;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM VM = new()
        {
            SliderItems = await _sliderItemService.GetAllShowedSliderItemsAsync(),
            PortfolioCards = await _portfolioService.GetAllShowedPortfolioCardsAsync(),
            Partners = await _partnerService.GetAllShowedPartnersAsync(),
            Blogs = await _blogService.GetAllShowedBlogsAsync(3)
        };

        return View(VM);
    }
}
