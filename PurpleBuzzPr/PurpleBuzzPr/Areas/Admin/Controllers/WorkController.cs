using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IActionResult> Index()
    {
        IEnumerable<Work> works = _db.Works.ToList();

        foreach (Work work in works)
        {
            work.Service = await _db.Services.FindAsync(work.ServiceId);
        }

        return View(works);
    }

    public IActionResult Create()
    {
        TempData["services"] = _db.Services.ToList();
        TempData["categories"] = _db.WorkCategories.ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Work work)
    {
        if (!ModelState.IsValid)
        {
            return Create();
        }

        work.CreatedAt = DateTime.Now;

        await _db.Works.AddAsync(work);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        Work? work = await _db.Works.FindAsync(id);
        if (work == null)
        {
            return NotFound("Work not found!");
        }

        TempData["services"] = _db.Services.ToList();
        TempData["categories"] = _db.WorkCategories.ToList();
        TempData["work"] = work;

        return View("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Work work)
    {
        if(!ModelState.IsValid)
        {
            return await Edit(work.Id);
        }

        Work? originalWork = await _db.Works.AsNoTracking().FirstOrDefaultAsync(w => w.Id == work.Id);
        if (originalWork == null)
        {
            return NotFound("Work not found!");
        }

        work.CreatedAt = originalWork.CreatedAt;
        work.UpdatedAt = DateTime.Now;

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
