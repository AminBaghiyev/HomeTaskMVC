using Mediplus.BL.DTOs.BlogDTOs;
using Mediplus.BL.DTOs.PartnerDTOs;
using Mediplus.BL.DTOs.PortfolioDTOs;
using Mediplus.BL.DTOs.SliderItemDTOs;

namespace Mediplus.PL.ViewModels.Home;

public class HomeVM
{
	public IEnumerable<ShowSliderItemDto>? SliderItems { get; set; }

	public IEnumerable<ShowPortfolioCardDto>? PortfolioCards { get; set; }

	public IEnumerable<ShowPartnerDto>? Partners { get; set; }

	public IEnumerable<ShowBlogDto>? Blogs { get; set; }
}
