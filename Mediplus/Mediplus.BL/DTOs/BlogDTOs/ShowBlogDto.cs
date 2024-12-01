using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.BlogDTOs;

public class ShowBlogDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string ThumbnailPath { get; set; }
	public DateTime? CreatedAt { get; set; }

	public static implicit operator ShowBlogDto(Blog item)
	{
		return new ShowBlogDto()
		{
			Id = item.Id,
			Title = item.Title,
			Description = item.Description,
			ThumbnailPath = item.ThumbnailPath,
			CreatedAt = item.CreatedAt
		};
	}
}
