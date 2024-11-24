using PurpleBuzzPr.Models.Base;

namespace PurpleBuzzPr.Models;

public class WorkCategory : BaseAuditableEntity
{
    public string Title { get; set; }

    public ICollection<Work> Works { get; set; } = [];
}
