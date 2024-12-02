using Inance.Areas.Admin.ViewModels;
using Inance.Contexts;
using Inance.DTOs.MasterDTOs;
using Inance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inance.Areas.Admin.Controllers;

[Area("Admin")]
public class MasterController : Controller
{
    readonly AppDbContext _db;

    public MasterController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<GetMasterDto> masters = (await _db.Masters.AsNoTracking().ToListAsync()).Select(m => (GetMasterDto) m).ToList();

        return View(masters);
    }

    public async Task<IActionResult> Create()
    {
        MasterVM VM = new()
        {
            Services = new (await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title))
        };

        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MasterVM vm)
    {
        GetMasterDto form = vm.Form;

        ModelState.Clear();
        TryValidateModel(form);

        if (!ModelState.IsValid)
        {
            MasterVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Form = form
            };
            return View(VM);
        }

        Master master = new()
        {
            Name = form.Name,
            Surname = form.Surname,
            Username = form.Username,
            PhoneNumber = form.PhoneNumber,
            Email = form.Email,
            ExperienceYear = form.ExperienceYear,
            ServiceId = form.ServiceId,
            IsActive = form.IsActive,
            CreatedAt = DateTime.Now
        };

        try
        {
            await _db.Masters.AddAsync(master);
            await _db.SaveChangesAsync();
        } catch (Exception ex)
        {
            ModelState.AddModelError("Form.ServiceId", "Something went wrong!");
            MasterVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Form = form
            };
            return View(VM);
        }
        

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Master? master = await _db.Masters.FindAsync(Id);
        if (master is null)
        {
            return RedirectToAction(nameof(Index));
        }

        MasterVM VM = new()
        {
            Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
            Form = master
        };

        return View("Create", VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(MasterVM vm)
    {
        GetMasterDto form = vm.Form;

        ModelState.Clear();
        TryValidateModel(form);

        if (!ModelState.IsValid)
        {
            MasterVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Form = form
            };
            return View("Create", VM);
        }

        Master? master = await _db.Masters.FindAsync(form.Id);
        if (master is null)
        {
            MasterVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Form = form
            };
            return View("Create", VM);
        }

        master.Name = form.Name;
        master.Surname = form.Surname;
        master.Username = form.Username;
        master.PhoneNumber = form.PhoneNumber;
        master.Email = form.Email;
        master.ExperienceYear = form.ExperienceYear;
        master.ServiceId = form.ServiceId;
        master.IsActive = form.IsActive;
        master.UpdatedAt = DateTime.Now;

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Master? master = await _db.Masters.FindAsync(Id);
        if (master is null)
        {
            return RedirectToAction(nameof(Index));
        }

        master.Service = await _db.Services.Where(s => s.Id == master.ServiceId).FirstOrDefaultAsync();
        master.Orders = await _db.Orders.Where(o => o.MasterId == Id).ToListAsync();

        return View(master);
    }
}
