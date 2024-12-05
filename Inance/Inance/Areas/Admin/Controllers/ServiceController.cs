using Inance.Contexts;
using Inance.DTOs.ServiceDTOs;
using Inance.Models;
using Inance.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inance.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ServiceController : Controller
{
    readonly AppDbContext _db;
    readonly IWebHostEnvironment _webHostEnvironment;

    public ServiceController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Service> services = await _db.Services.AsNoTracking().ToListAsync();

        return View(services);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateServiceDto createService)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (createService.Thumbnail is not null && !createService.Thumbnail.CheckType("image"))
        {
            ModelState.AddModelError("Thumbnail", "File type must be image!");
            return View(createService);
        }

        foreach (IFormFile photo in createService.Photos ?? [])
        {
            if (!photo.CheckType("image"))
            {
                ModelState.AddModelError("Photos", "File type must be image!");
                return View(createService);
            }
        }

        string? thumbnailPath = createService.Thumbnail is null ? null : await createService.Thumbnail.SaveAsync(_webHostEnvironment.WebRootPath, "serviceImages");

        Service service = new()
        {
            Title = createService.Title,
            Description = createService.Description,
            ThumbnailPath = thumbnailPath,
            IsActive = createService.IsActive,
            CreatedAt = DateTime.Now
        };

        ICollection<ServicePhoto> Photos = [];
        foreach (IFormFile photo in createService.Photos ?? [])
        {
            Photos.Add(new()
            {
                ImagePath = await photo.SaveAsync(_webHostEnvironment.WebRootPath, "serviceImages"),
                Service = service
            });
        }

        await _db.Services.AddAsync(service);
        await _db.ServicePhotos.AddRangeAsync(Photos);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service is null)
        {
            return RedirectToAction(nameof(Index));
        }

        service.Photos = await _db.ServicePhotos.Where(p => p.ServiceId == Id).ToListAsync();

        return View((UpdateServiceDto) service);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateServiceDto serviceDto)
    {
        Service? service = await _db.Services.FindAsync(serviceDto.Id);
        if (service is null)
        {
            return View(serviceDto);
        }

        if (!ModelState.IsValid)
        {
            serviceDto.PhotosPaths = await _db.ServicePhotos.Where(p => p.ServiceId == serviceDto.Id).ToListAsync();
            serviceDto.ThumbnailPath = service.ThumbnailPath;

            return View(serviceDto);
        }

        if (serviceDto.Thumbnail is not null && !serviceDto.Thumbnail.CheckType("image"))
        {
            serviceDto.PhotosPaths = await _db.ServicePhotos.Where(p => p.ServiceId == serviceDto.Id).ToListAsync();
            serviceDto.ThumbnailPath = service.ThumbnailPath;

            ModelState.AddModelError("Thumbnail", "File type must be image!");
            return View(serviceDto);
        }
        foreach (IFormFile photo in serviceDto.Photos ?? [])
        {
            if (!photo.CheckType("image"))
            {
                ModelState.AddModelError("Photos", "File type must be image!");
                return View(serviceDto);
            }
        }

        if (service.ThumbnailPath is not null && serviceDto.Thumbnail is not null)
        {
            serviceDto.PhotosPaths = await _db.ServicePhotos.Where(p => p.ServiceId == serviceDto.Id).ToListAsync();
            serviceDto.ThumbnailPath = service.ThumbnailPath;

            ModelState.AddModelError("Thumbnail", "This service has a thumbnail");
            return View(serviceDto);
        }

        if (serviceDto.Thumbnail is not null)
        {
            service.ThumbnailPath = await serviceDto.Thumbnail.SaveAsync(_webHostEnvironment.WebRootPath, "serviceImages");
        }

        ICollection<ServicePhoto> Photos = [];
        foreach (IFormFile photo in serviceDto.Photos ?? [])
        {
            Photos.Add(new()
            {
                ImagePath = await photo.SaveAsync(_webHostEnvironment.WebRootPath, "serviceImages"),
                Service = service
            });
        }

        service.Title = serviceDto.Title;
        service.Description = serviceDto.Description;
        service.IsActive = serviceDto.IsActive;
        service.UpdatedAt = DateTime.Now;

        await _db.ServicePhotos.AddRangeAsync(Photos);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service is null)
        {
            return RedirectToAction(nameof(Index));
        }

        service.Masters = await _db.Masters.Where(m => m.ServiceId == Id).ToListAsync();
        service.Orders = await _db.Orders.Where(o => o.ServiceId == Id).ToListAsync();
        service.Photos = await _db.ServicePhotos.Where(p => p.ServiceId == Id).ToListAsync();

        return View(service);
    }

    public async Task<IActionResult> RemovePhoto(int Id)
    {
        ServicePhoto? photo = await _db.ServicePhotos.FindAsync(Id);
        if (photo is null)
        {
            return RedirectToAction(nameof(Index));
        }

        System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "serviceImages", photo.ImagePath));

        _db.ServicePhotos.Remove(photo);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Update), new { Id = photo.ServiceId });
    }

    public async Task<IActionResult> RemoveThumbnail(int Id)
    {
        Service? service = await _db.Services.FindAsync(Id);
        if (service is null)
        {
            return RedirectToAction(nameof(Index));
        }

        System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "serviceImages", service.ThumbnailPath));

        service.ThumbnailPath = null;
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Update), new { Id });
    }
}
