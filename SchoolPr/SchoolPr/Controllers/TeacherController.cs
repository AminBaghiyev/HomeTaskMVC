using Microsoft.AspNetCore.Mvc;
using SchoolPr.Models;

namespace SchoolPr.Controllers;

public class TeacherController : Controller
{
    public ViewResult Index()
    {
        List<Teacher> teachers = [];

        teachers.Add(new Teacher()
        {
            FirstName = "Narmin",
            LastName = "Shivakhanova"
        });
        teachers.Add(new Teacher()
        {
            FirstName = "Vilayat",
            LastName = "Aliyev"
        });

        return View(teachers);
    }
}
