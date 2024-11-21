namespace PurpleBuzzPr.Models;

public class TeamMember : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string JobTitle { get; set; }
    public string ImagePath { get; set; }

    public string FullName => $"{Name} {Surname}";
}
