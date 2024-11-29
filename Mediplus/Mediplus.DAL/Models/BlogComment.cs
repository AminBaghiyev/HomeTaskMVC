using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class BlogComment : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
}
