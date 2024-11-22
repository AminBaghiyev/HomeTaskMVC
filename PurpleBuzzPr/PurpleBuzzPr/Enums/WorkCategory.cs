using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzPr.Enums;

public enum WorkCategory
{
    All,
    Business,
    Marketing,
    [Display(Name = "Social Media")]
    SocialMedia,
    Graphic
}
