using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class PortfolioCategory : BaseAuditableEntity
{
    public string Title { get; set; }
    public ICollection<Portfolio> Portfolios { get; } = [];
    public bool IsActive { get; set; } = true;
}
