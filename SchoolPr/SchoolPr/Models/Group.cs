namespace SchoolPr.Models;

public class Group : BaseEntity
{
    private static int _autoIncrement = 0;
    public string Name { get; set; }

    public Group()
    {
        Id = ++_autoIncrement;
    }
}
