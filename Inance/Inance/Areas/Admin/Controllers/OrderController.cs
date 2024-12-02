using Inance.Areas.Admin.ViewModels;
using Inance.Contexts;
using Inance.DTOs.OrderDTOs;
using Inance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inance.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderController : Controller
{
    readonly AppDbContext _db;

    public OrderController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<GetOrderDto> orders = (await _db.Orders.AsNoTracking().ToListAsync()).Select(m => (GetOrderDto) m).ToList();

        return View(orders);
    }

    public async Task<IActionResult> Create()
    {
        OrderVM VM = new()
        {
            Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title))
        };

        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderVM vm)
    {
        FormOrderDto form = vm.Form;

        ModelState.Clear();
        TryValidateModel(form);

        if (!ModelState.IsValid)
        {
            OrderVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Form = form
            };
            return View(VM);
        }

        Master? master = await _db.Masters
            .Where(m => m.IsActive && m.ServiceId == form.ServiceId)
            .OrderByDescending(m => m.ExperienceYear)
            .FirstOrDefaultAsync();
        
        if (master is null)
        {
            ModelState.AddModelError("ServiceId", "No suitable master found for this service");
            OrderVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Form = form
            };
            return View(VM);
        }

        Order order = new()
        {
            ClientName = form.ClientName,
            ClientSurname = form.ClientSurname,
            ClientPhoneNumber = form.ClientPhoneNumber,
            ClientEmail = form.ClientEmail,
            MasterId = master.Id,
            ServiceId = form.ServiceId,
            Problem = form.Problem,
            IsActive = form.IsActive,
            CreatedAt = DateTime.Now
        };

        try
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Form.ServiceId", "Something went wrong!");
            OrderVM VM = new()
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
        Order? order = await _db.Orders.FindAsync(Id);
        if (order is null)
        {
            return RedirectToAction(nameof(Index));
        }

        OrderVM VM = new()
        {
            Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
            Master = await _db.Masters.FindAsync(order.MasterId),
            Form = order
        };

        return View("Create", VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(OrderVM vm)
    {
        FormOrderDto form = vm.Form;

        ModelState.Clear();
        TryValidateModel(form);

        if (!ModelState.IsValid)
        {
            OrderVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Master = await _db.Masters.FindAsync(form.MasterId),
                Form = form
            };
            return View("Create", VM);
        }

        Order? order = await _db.Orders.FindAsync(form.Id);
        if (order is null)
        {
            OrderVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Master = await _db.Masters.FindAsync(form.MasterId),
                Form = form
            };
            return View("Create", VM);
        }

        order.ClientName = form.ClientName;
        order.ClientSurname = form.ClientSurname;
        order.ClientPhoneNumber = form.ClientPhoneNumber;
        order.ClientEmail = form.ClientEmail;
        order.MasterId = form.MasterId;
        order.ServiceId = form.ServiceId;
        order.Problem = form.Problem;
        order.IsActive = form.IsActive;
        order.UpdatedAt = DateTime.Now;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Form.ServiceId", "Something went wrong!");
            OrderVM VM = new()
            {
                Services = new(await _db.Services.Where(s => s.IsActive).ToListAsync(), nameof(Service.Id), nameof(Service.Title)),
                Master = await _db.Masters.FindAsync(form.MasterId),
                Form = form
            };
            return View(VM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Order? order = await _db.Orders.FindAsync(Id);
        if (order is null)
        {
            return RedirectToAction(nameof(Index));
        }

        order.Service = await _db.Services.Where(s => s.Id == order.ServiceId).FirstOrDefaultAsync();
        order.Master = await _db.Masters.Where(s => s.Id == order.MasterId).FirstOrDefaultAsync();

        return View(order);
    }
}
