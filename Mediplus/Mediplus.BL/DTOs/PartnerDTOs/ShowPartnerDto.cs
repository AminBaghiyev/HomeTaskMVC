using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.PartnerDTOs;

public class ShowPartnerDto
{
	public string Title { get; set; }
	public string? Url { get; set; }
	public string LogoPath { get; set; }

	public static implicit operator ShowPartnerDto(Partner item)
	{
		return new ShowPartnerDto()
		{
			Title = item.Title,
			Url = item.Url,
			LogoPath = item.LogoPath
		};
	}
}
