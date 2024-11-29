using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class BlogCategory : BaseAuditableEntity
{
    public string Title { get; set; }
    public ICollection<Blog> Blogs { get; } = [];
    public bool IsActive { get; set; } = true;
}
