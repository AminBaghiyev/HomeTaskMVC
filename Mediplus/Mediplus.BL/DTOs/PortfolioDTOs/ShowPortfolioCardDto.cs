using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.PortfolioDTOs;

public class ShowPortfolioCardDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string ThumbnailPath { get; set; }

	public static implicit operator ShowPortfolioCardDto(Portfolio item)
	{
		return new ShowPortfolioCardDto()
		{
			Id = item.Id,
			Title = item.Title,
			ThumbnailPath = item.ThumbnailPath
		};
	}
}
