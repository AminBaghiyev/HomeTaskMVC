using Inance.Models.Base;

namespace Inance.Models;

public class Service : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ThumbnailPath { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Master> Masters { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<ServicePhoto>? Photos { get; set; }
}
