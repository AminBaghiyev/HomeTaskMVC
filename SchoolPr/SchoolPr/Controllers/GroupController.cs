using Microsoft.AspNetCore.Mvc;
using SchoolPr.Models;

namespace SchoolPr.Controllers;

public class GroupController : Controller
{
    public ViewResult Index()
    {
        List<Group> groups = [];

        groups.Add(new Group()
        {
            Name = "AB205"
        });
        groups.Add(new Group()
        {
            Name = "AB505"
        });
        groups.Add(new Group()
        {
            Name = "AB101"
        });

        return View(groups);
    }
}
