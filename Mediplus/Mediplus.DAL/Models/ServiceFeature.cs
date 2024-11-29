using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class ServiceFeature : BaseAuditableEntity
{
    public string Title { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Service> ServiceItems { get; } = [];
    public ICollection<ServicesFeatures> ServiceItemFeatures { get; } = [];
}
