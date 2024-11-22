using PurpleBuzzPr.Enums;

namespace PurpleBuzzPr.Models;

public class Work : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailPath { get; set; }
    public WorkCategory Category { get; set; }
}
