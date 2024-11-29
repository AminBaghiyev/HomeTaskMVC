using Mediplus.BL.DTOs.PortfolioDTOs;
using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Concretes;

public interface IPortfolioService
{
	Task<IEnumerable<ShowPortfolioCardDto>> GetAllShowedPortfolioCardsAsync();
	Task<List<Portfolio>> GetAllPortfoliosAsync();
	Task<Portfolio?> GetPortfolioByIdAsync(int Id);
	Task CreatePortfolioAsync(Portfolio portfolio);
	Task UpdatePortfolioAsync(int Id, Portfolio portfolio);
	Task DeletePortfolioAsync(int Id);
}
