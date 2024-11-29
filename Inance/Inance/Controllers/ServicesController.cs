using Microsoft.AspNetCore.Mvc;

namespace Inance.Controllers;

public class ServicesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
