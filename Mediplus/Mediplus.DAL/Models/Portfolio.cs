using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Portfolio : BaseAuditableEntity
{
    public string ThumbnailPath { get; set; }
    //public string MainImagePath { get; set; }
    public string Title { get; set; }
    //public string ClientName { get; set; }
    //public string BodyText { get; set; }
    public bool IsActive { get; set; } = true;
    //public int CategoryId { get; set; }
    //public PortfolioCategory Category { get; set; } = null!;
}
