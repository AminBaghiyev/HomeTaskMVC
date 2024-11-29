using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Tag : BaseEntity
{
    public string Title { get; set; }
    public ICollection<Blog> Blogs { get; } = [];
    public ICollection<BlogsTags> BlogsTag { get; set; } = [];
}
