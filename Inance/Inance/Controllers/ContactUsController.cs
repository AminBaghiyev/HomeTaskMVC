using Microsoft.AspNetCore.Mvc;

namespace Inance.Controllers;

public class ContactUsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
