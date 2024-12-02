using Inance.Contexts;
using Inance.DTOs.ServiceDTOs;
using Inance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inance.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ServiceController : Controller
{
    readonly AppDbContext _db;

    public ServiceController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<GetServiceDto> services = await _db.Services.AsNoTracking().Select(s => new GetServiceDto()
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            IsActive = s.IsActive
        }).ToListAsync();

        return View(services);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GetServiceDto serviceDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Service service = new()
        {
            Title = serviceDto.Title,
            Description = serviceDto.Description,
            IsActive = serviceDto.IsActive,
            CreatedAt = DateTime.Now
        };

        await _db.Services.AddAsync(service);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service == null)
        {
            return RedirectToAction(nameof(Index));
        }

        GetServiceDto serviceDto = new()
        {
            Title = service.Title,
            Description = service.Description,
            IsActive = service.IsActive
        };

        return View("Create", serviceDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(GetServiceDto serviceDto)
    {
        if (!ModelState.IsValid)
        {
            return View("Create");
        }

        Service? service = await _db.Services.FindAsync(serviceDto.Id);
        if (service == null)
        {
            return View("Create");
        }

        service.Title = serviceDto.Title;
        service.Description = serviceDto.Description;
        service.IsActive = serviceDto.IsActive;
        service.UpdatedAt = DateTime.Now;

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service == null)
        {
            return RedirectToAction(nameof(Index));
        }

        GetServiceDetailsDto serviceDto = new()
        {
            Title= service.Title,
            Description= service.Description,
            IsActive = service.IsActive,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt,
            Masters = await _db.Masters.Where(m => m.ServiceId == Id).ToListAsync(),
            Orders = await _db.Orders.Where(m => m.ServiceId == Id).ToListAsync()
        };

        return View(serviceDto);
    }
}
