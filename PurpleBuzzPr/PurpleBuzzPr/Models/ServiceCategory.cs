using PurpleBuzzPr.Models.Base;

namespace PurpleBuzzPr.Models;

public class ServiceCategory : BaseAuditableEntity
{
    public string Title { get; set; }

    public ICollection<Service> Services { get; set; } = [];
}
