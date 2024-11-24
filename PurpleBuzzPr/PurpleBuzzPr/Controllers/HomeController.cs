using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.ViewsModel.Home;

namespace PurpleBuzzPr.Controllers;

public class HomeController : Controller
{
    private readonly AppDBContext _db;

    public HomeController(AppDBContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        HomeVM VM = new()
        {
            Services = _db.Services.ToList(),
        };

        return View(VM);
    }
}
