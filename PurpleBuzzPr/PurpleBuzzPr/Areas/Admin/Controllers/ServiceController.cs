using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.Areas.Admin.Controllers;

[Area("Admin")]
public class ServiceController : Controller
{
    private readonly AppDBContext _db;

    public ServiceController(AppDBContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Service> services = _db.Services.ToList();

        return View(services);
    }

    public IActionResult Create()
    {
        TempData["categories"] = _db.ServiceCategories.ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Service service)
    {
        if (!ModelState.IsValid)
        {
            return Create();
        }

        service.CreatedAt = DateTime.Now;

        await _db.Services.AddAsync(service);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service == null)
        {
            return NotFound("Service not found!");
        }

        TempData["categories"] = _db.ServiceCategories.ToList();
        TempData["service"] = service;

        return View("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Service service)
    {
        if (!ModelState.IsValid)
        {
            return await Edit(service.Id);
        }

        Service? originalService = await _db.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == service.Id);
        if (originalService == null)
        {
            return NotFound("Service not found!");
        }

        service.CreatedAt = originalService.CreatedAt;
        service.UpdatedAt = DateTime.Now;

        _db.Services.Update(service);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service == null)
        {
            return NotFound("Service not found!");
        }

        try
        {
            _db.Services.Remove(service);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}
