using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Service : BaseAuditableEntity
{
    public string Title { get; set; }
    public int Price { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Service> ServiceItems { get; } = [];
    public ICollection<ServicesFeatures> ServiceItemFeatures { get; } = [];
}
