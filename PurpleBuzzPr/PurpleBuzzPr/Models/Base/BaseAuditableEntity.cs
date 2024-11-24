using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.Models.Base;

public class BaseAuditableEntity : BaseEntity
{
    [AllowNull]
    public DateTime? CreatedAt { get; set; }

    [AllowNull]
    public DateTime? UpdatedAt { get; set; }
}
