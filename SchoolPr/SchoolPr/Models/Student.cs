namespace SchoolPr.Models;

public class Student : BaseEntity
{
    private static int _autoIncrement = 0;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int GroupId { get; set; }

    public Student()
    {
        Id = ++_autoIncrement;
    }
}
