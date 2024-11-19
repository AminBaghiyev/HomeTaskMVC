using Microsoft.AspNetCore.Mvc;
using SchoolPr.Models;

namespace SchoolPr.Controllers;

public class StudentController : Controller
{
    public ViewResult Index()
    {
        List<Student> students = [];

        students.Add(new Student()
        {
            FirstName = "Amin",
            LastName = "Baghiyev",
            GroupId = 1
        });
        students.Add(new Student()
        {
            FirstName = "Elvin",
            LastName = "Aliyev",
            GroupId = 2
        });

        students.Add(new Student()
        {
            FirstName = "Leyla",
            LastName = "Huseynova",
            GroupId = 1
        });

        students.Add(new Student()
        {
            FirstName = "Nurlan",
            LastName = "Mammadov",
            GroupId = 3
        });

        students.Add(new Student()
        {
            FirstName = "Aygun",
            LastName = "Qurbanova",
            GroupId = 2
        });

        students.Add(new Student()
        {
            FirstName = "Kamran",
            LastName = "Ibrahimov",
            GroupId = 1
        });
        students.Add(new Student()
        {
            FirstName = "Sevinj",
            LastName = "Guliyeva",
            GroupId = 3
        });
        students.Add(new Student()
        {
            FirstName = "Murad",
            LastName = "Shukurov",
            GroupId = 2
        });
        students.Add(new Student()
        {
            FirstName = "Tural",
            LastName = "Ismayilov",
            GroupId = 1
        });
        students.Add(new Student()
        {
            FirstName = "Sabina",
            LastName = "Mustafayeva",
            GroupId = 3
        });


        return View(students);
    }
}
