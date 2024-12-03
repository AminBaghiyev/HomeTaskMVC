using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Mediplus.PL.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class HospitalController : Controller
{
    readonly IBaseService<Hospital> _hospitalService;
    readonly IBaseService<Doctor> _doctorService;
    readonly IHospitalsDoctorsService _hospitalsDoctorsService;

    public HospitalController(IBaseService<Hospital> hospitalService, IBaseService<Doctor> doctorService, IHospitalsDoctorsService hospitalsDoctorsService)
    {
        _hospitalService = hospitalService;
        _doctorService = doctorService;
        _hospitalsDoctorsService = hospitalsDoctorsService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Hospital> hospitals = await _hospitalService.GetAllAsync();

        return View(hospitals);
    }

    public async Task<IActionResult> Create()
    {
        CreateHospitalVM VM = new()
        {
            DoctorsList = new(await _doctorService.GetAllActiveAsync(), nameof(Doctor.Id), nameof(Doctor.Fullname))
        };

        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHospitalVM form)
    {
        if (!ModelState.IsValid)
        {
            CreateHospitalVM VM = new()
            {
                DoctorsList = new(await _doctorService.GetAllActiveAsync(), nameof(Doctor.Id), nameof(Doctor.Fullname))
            };
            return View(VM);
        }

        if (form.DoctorsIds is not null)
        {
            foreach (int id in form.DoctorsIds)
            {
                if (await _doctorService.GetByIdAsync(id) is null)
                {
                    CreateHospitalVM VM = new()
                    {
                        DoctorsList = new(await _doctorService.GetAllActiveAsync(), nameof(Doctor.Id), nameof(Doctor.Fullname))
                    };
                    return View(VM);
                }
            }
        }

        Hospital hospital = new()
        {
            Name = form.Name,
            Address = form.Address,
            PhoneNumber = form.PhoneNumber,
            Email = form.Email,
            IsActive = form.IsActive
        };

        await _hospitalService.CreateAsync(hospital);

        if (form.DoctorsIds is not null)
        {
            foreach (var id in form.DoctorsIds)
            {
                await _hospitalsDoctorsService.CreateAsync(new HospitalsDoctors
                {
                    Hospital = hospital,
                    DoctorId = id
                });
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        Hospital? hospital = await _hospitalService.GetByIdAsNoTrackingAsync(id);
        if (hospital is null)
        {
            return RedirectToAction(nameof(Index));
        }

        UpdateHospitalVM VM = hospital;
        VM.DoctorsList = new(await _doctorService.GetAllActiveAsync(), nameof(Doctor.Id), nameof(Doctor.Fullname));
        VM.DoctorsIds = await _hospitalsDoctorsService.GetIdsByHospitalIdAsync(id);

        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateHospitalVM VM)
    {
        if (!ModelState.IsValid)
        {
            VM.DoctorsList = new(await _doctorService.GetAllActiveAsync(), nameof(Doctor.Id), nameof(Doctor.Fullname));
            return View(VM);
        }

        Hospital hospital = new()
        {
            Id = VM.Id,
            Name = VM.Name,
            Address = VM.Address,
            PhoneNumber = VM.PhoneNumber,
            Email = VM.Email,
            IsActive = VM.IsActive
        };

        await _hospitalService.UpdateAsync(VM.Id, hospital);

        List<int> existingDoctorsIds = await _hospitalsDoctorsService.GetIdsByHospitalIdAsync(VM.Id);
        if (VM.DoctorsIds is not null)
        {
            List<int> deletedDoctorsIds = existingDoctorsIds.Where(id => !VM.DoctorsIds.Contains(id)).ToList();
            List<int> addedDoctorsIds = VM.DoctorsIds.Where(id => !existingDoctorsIds.Contains(id)).ToList();

            if (deletedDoctorsIds.Count != 0)
            {
                foreach (int id in deletedDoctorsIds)
                {
                    await _hospitalsDoctorsService.DeleteAsync(VM.Id, id);
                }
            }

            if (addedDoctorsIds.Count != 0)
            {
                foreach (int id in addedDoctorsIds)
                {
                    await _hospitalsDoctorsService.CreateAsync(new HospitalsDoctors
                    {
                        HospitalId = VM.Id,
                        DoctorId = id
                    });
                }
            }
        } else
        {
            foreach (int id in existingDoctorsIds)
            {
                await _hospitalsDoctorsService.DeleteAsync(VM.Id, id);
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _hospitalService.DeleteAsync(id);
        } catch (Exception) { }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        Hospital? hospital = await _hospitalService.GetByIdAsNoTrackingAsync(id);
        if (hospital is null)
        {
            return RedirectToAction(nameof(Index));
        }

        hospital.Doctors = await _hospitalsDoctorsService.GetByHospitalIdAsync(id) ?? [];

        return View(hospital);
    }
}
