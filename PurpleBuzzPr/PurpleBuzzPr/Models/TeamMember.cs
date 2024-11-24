using PurpleBuzzPr.Models.Base;

namespace PurpleBuzzPr.Models;

public class TeamMember : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string JobTitle { get; set; }
    public string ImagePath { get; set; }

    public string FullName => $"{Name} {Surname}";
}
