using Mediplus.BL.DTOs.PortfolioDTOs;
using Mediplus.BL.DTOs.SliderItemDTOs;
using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Abstractions;

public class PortfolioService : IPortfolioService
{
	readonly AppDbContext _db;
	public PortfolioService(AppDbContext db)
	{
		_db = db;
	}

	public async Task CreatePortfolioAsync(Portfolio portfolio)
	{
		portfolio.CreatedAt = DateTime.Now;
		await _db.Portfolios.AddAsync(portfolio);
		await _db.SaveChangesAsync();
	}

	public async Task DeletePortfolioAsync(int Id)
	{
		Portfolio? portfolio = await GetPortfolioByIdAsync(Id);
		if (portfolio == null)
		{
			return;
		}

		_db.Portfolios.Remove(portfolio);
		await _db.SaveChangesAsync();
	}

	public async Task<List<Portfolio>> GetAllPortfoliosAsync() => await _db.Portfolios.ToListAsync();

	public async Task<IEnumerable<ShowPortfolioCardDto>> GetAllShowedPortfolioCardsAsync()
	{
		return await _db.Portfolios
			.AsNoTracking()
			.Where(item => item.IsActive)
			.Select(item => new ShowPortfolioCardDto
			{
				Id = item.Id,
				Title = item.Title,
				ThumbnailPath = item.ThumbnailPath
			})
			.ToListAsync();
	}

	public async Task<Portfolio?> GetPortfolioByIdAsync(int Id) => await _db.Portfolios.FindAsync(Id);

	public async Task UpdatePortfolioAsync(int Id, Portfolio updatedPortfolio)
	{
		Portfolio? portfolio = await GetPortfolioByIdAsync(Id);
		if (portfolio == null)
		{
			return;
		}

		portfolio.ThumbnailPath = updatedPortfolio.ThumbnailPath;
		portfolio.Title = updatedPortfolio.Title;
		portfolio.IsActive = updatedPortfolio.IsActive;
		portfolio.UpdatedAt = DateTime.Now;

		await _db.SaveChangesAsync();
	}
}
