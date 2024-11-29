namespace Mediplus.DAL.Models;

public class ServicesFeatures
{
    public int ServiceId { get; set; }
    public int FeatureId { get; set; }
    public Service ServiceItem { get; set; } = null!;
    public ServiceFeature Feature { get; set; } = null!;
}
