using PurpleBuzzPr.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.Models;

public class Service : BaseAuditableEntity
{
    [DisallowNull]
    public string Title { get; set; }

    [DisallowNull]
    public string Description { get; set; }

    [DisallowNull]
    public string ThumbnailPath { get; set; }

    [AllowNull]
    public ICollection<Work> Works { get; set; } = [];

    [AllowNull]
    public int? CategoryId { get; set; }

    [AllowNull]
    public ServiceCategory? Category { get; set; }
}
