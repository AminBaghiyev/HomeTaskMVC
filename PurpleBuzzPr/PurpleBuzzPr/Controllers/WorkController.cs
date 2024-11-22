using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DAL;

namespace PurpleBuzzPr.Controllers;

public class WorkController : Controller
{
    private readonly AppDBContext _db;

    public WorkController(AppDBContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View(_db.Works);
    }

    // I have a question
    public IActionResult SingleWork(int id)
    {
        return View();
    }
    // I have a question
}
