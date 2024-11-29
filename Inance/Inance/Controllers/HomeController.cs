using Microsoft.AspNetCore.Mvc;

namespace Inance.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
