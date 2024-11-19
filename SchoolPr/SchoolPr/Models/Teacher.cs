namespace SchoolPr.Models;

public class Teacher : BaseEntity
{
    private static int _autoIncrement = 0;
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Teacher()
    {
        Id = ++_autoIncrement;
    }
}
