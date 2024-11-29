using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class SliderItem : BaseAuditableEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? MainUrl { get; set; }
    public string? SecondUrl { get; set; }
    public string BackgroundImagePath { get; set; }
    public bool IsActive { get; set; } = true;
}
