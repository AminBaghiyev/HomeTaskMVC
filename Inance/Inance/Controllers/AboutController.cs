using Microsoft.AspNetCore.Mvc;

namespace Inance.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
