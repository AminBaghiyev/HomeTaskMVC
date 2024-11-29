using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Blog : BaseAuditableEntity
{
    //public string AuthorName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    //public string BodyText { get; set; }
    public string ThumbnailPath { get; set; }
    //public int Views { get; set; }
    public bool IsActive { get; set; } = true;
    //public int CategoryId { get; set; }
    //public BlogCategory Category { get; set; } = null!;
    //public ICollection<BlogComment> Comments { get; } = [];
    //public ICollection<Tag> Tags { get; } = [];
}
