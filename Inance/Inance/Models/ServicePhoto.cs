using Inance.Models.Base;

namespace Inance.Models;

public class ServicePhoto : BaseEntity
{
    public string ImagePath { get; set; }
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}
