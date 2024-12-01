using Mediplus.BL.DTOs.AppointmentDTOs;
using Mediplus.BL.DTOs.BlogDTOs;
using Mediplus.BL.DTOs.PartnerDTOs;
using Mediplus.BL.DTOs.PortfolioDTOs;
using Mediplus.BL.DTOs.SliderItemDTOs;
using Mediplus.DAL.Models;

namespace Mediplus.PL.ViewModels.Home;

public class HomeVM
{
	public IEnumerable<ShowSliderItemDto>? SliderItems { get; set; }

	public IEnumerable<ShowPortfolioCardDto>? PortfolioCards { get; set; }

	public IEnumerable<ShowPartnerDto>? Partners { get; set; }

	public IEnumerable<ShowBlogDto>? Blogs { get; set; }

	public Dictionary<string, string?> Settings { get; set; }

	public FormUserAppointmentDto AppointmentForm { get; set; }
}
