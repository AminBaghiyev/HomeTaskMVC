using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Partner : BaseAuditableEntity
{
    public string Title { get; set; }
    public string? Url { get; set; }
    public string LogoPath { get; set; }
}
