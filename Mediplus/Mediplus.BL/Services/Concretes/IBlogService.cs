using Mediplus.BL.DTOs.BlogDTOs;
using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Concretes;

public interface IBlogService
{
	Task<IEnumerable<ShowBlogDto>> GetAllShowedBlogsAsync(int count);
	Task<List<Blog>> GetAllBlogsAsync();
	Task<Blog?> GetBlogByIdAsync(int Id);
	Task CreateBlogAsync(Blog portfolio);
	Task UpdateBlogAsync(int Id, Blog portfolio);
	Task DeleteBlogAsync(int Id);
}
