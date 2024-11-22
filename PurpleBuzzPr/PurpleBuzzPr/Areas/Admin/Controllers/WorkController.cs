using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.Areas.Admin.Controllers;

[Area("Admin")]
public class WorkController : Controller
{
    private readonly AppDBContext _db;

    public WorkController(AppDBContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Work> works = _db.Works;

        return View(works);
    }

    public IActionResult Create()
    {
        return View(new Work());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Work work)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Wrong input fields!");
        }

        await _db.Works.AddAsync(work);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        Work? work = await _db.Works.FindAsync(id);
        if (work == null)
        {
            return NotFound();
        }

        return View("Create", work);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Work work)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Someting went wrong!");
        }

        _db.Works.Update(work);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int id)
    {
        Work? work = await _db.Works.FindAsync(id);
        if (work == null)
        {
            return NotFound();
        }

        _db.Works.Remove(work);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
