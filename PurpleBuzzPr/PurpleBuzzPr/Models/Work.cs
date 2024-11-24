using PurpleBuzzPr.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.Models;

public class Work : BaseAuditableEntity
{
    [DisallowNull]
    public string Title { get; set; }

    [DisallowNull]
    public string Description { get; set; }

    [DisallowNull]
    public string ThumbnailPath { get; set; }

    [DisallowNull]
    [MinLength(10, ErrorMessage = "Content length must be at least 10 symbols")]
    public string Content { get; set; }

    [AllowNull]
    public int? CategoryId { get; set; }

    [AllowNull]
    public WorkCategory? Category { get; set; }

    [AllowNull]
    public int? ServiceId { get; set; }

    [AllowNull]
    public Service? Service { get; set; }
}
