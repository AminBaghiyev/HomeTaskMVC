using Microsoft.AspNetCore.Mvc;

namespace SchoolPr.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        return View();
    }
}
