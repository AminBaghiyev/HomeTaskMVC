using Mediplus.BL.DTOs.BlogDTOs;
using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Abstractions;

public class BlogService : IBlogService
{
	readonly AppDbContext _db;
	public BlogService(AppDbContext db)
	{
		_db = db;
	}

	public async Task CreateBlogAsync(Blog blog)
	{
		blog.CreatedAt = DateTime.Now;
		await _db.Blogs.AddAsync(blog);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteBlogAsync(int Id)
	{
		Blog? blog = await GetBlogByIdAsync(Id);
		if (blog == null)
		{
			return;
		}

		_db.Blogs.Remove(blog);
		await _db.SaveChangesAsync();
	}

	public async Task<List<Blog>> GetAllBlogsAsync() => await _db.Blogs.ToListAsync();

	public async Task<Blog?> GetBlogByIdAsync(int Id) => await _db.Blogs.FindAsync(Id);

	public async Task<IEnumerable<ShowBlogDto>> GetAllShowedBlogsAsync(int count = 3)
	{
		return await _db.Blogs
			.AsNoTracking()
			.Where(x => x.IsActive)
			.Take(count)
			.Select(item => new ShowBlogDto
			{
				Id = item.Id,
				Title = item.Title,
				Description = item.Description,
				ThumbnailPath = item.ThumbnailPath,
				CreatedAt = item.CreatedAt
			})
			.ToListAsync();
	}

	public async Task UpdateBlogAsync(int Id, Blog updatedBlog)
	{
		Blog? blog = await GetBlogByIdAsync(Id);
		if (blog == null)
		{
			return;
		}

		blog.Title = updatedBlog.Title;
		blog.Description = updatedBlog.Description;
		blog.ThumbnailPath = updatedBlog.ThumbnailPath;
		blog.IsActive = updatedBlog.IsActive;
		blog.UpdatedAt = DateTime.Now;

		await _db.SaveChangesAsync();
	}
}
