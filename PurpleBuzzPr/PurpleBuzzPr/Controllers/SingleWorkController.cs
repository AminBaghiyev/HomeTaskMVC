using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.Controllers;

public class SingleWorkController : Controller
{
    private readonly AppDBContext _db;

    public SingleWorkController(AppDBContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(int id)
    {
        Work? work = await _db.Works.FindAsync(id);
        if (work == null)
        {
            return NotFound("Work not found!");
        }

        return View(work);
    }
}
