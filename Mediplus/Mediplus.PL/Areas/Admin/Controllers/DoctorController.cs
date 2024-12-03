using Mediplus.BL.DTOs.DoctorDTOs;
using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class DoctorController : Controller
{
    readonly IBaseService<Doctor> _doctorService;
    readonly IAppointmentService _appointmentService;
    readonly IHospitalsDoctorsService _hospitalsDoctorsService;

    public DoctorController(IBaseService<Doctor> doctorService, IAppointmentService appointmentService, IHospitalsDoctorsService hospitalsDoctorsService)
    {
        _doctorService = doctorService;
        _appointmentService = appointmentService;
        _hospitalsDoctorsService = hospitalsDoctorsService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<MinInfoPatientDto> doctors = (await _doctorService.GetAllAsync()).Select(i => (MinInfoPatientDto) i).ToList();

        return View(doctors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FormDoctorDto item)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Doctor doctor = new()
        {
            Name = item.Name,
            Surname = item.Surname,
            Username = item.Name + item.FINCode,
            PhoneNumber = item.PhoneNumber,
            Email = item.Email,
            FINCode = item.FINCode,
            IsActive = item.IsActive
        };

        await _doctorService.CreateAsync(doctor);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Doctor? doctor = await _doctorService.GetByIdAsync(Id);
        if (doctor == null)
        {
            return RedirectToAction(nameof(Index));
        }

        FormDoctorDto doctorDto = doctor;

        return View(nameof(Create), doctorDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(FormDoctorDto item)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Create), item);
        }

        Doctor doctor = new()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname,
            Username = item.Name + item.FINCode,
            PhoneNumber = item.PhoneNumber,
            Email = item.Email,
            FINCode = item.FINCode,
            IsActive = item.IsActive
        };

        await _doctorService.UpdateAsync(item.Id, doctor);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int Id)
    {
        await _doctorService.DeleteAsync(Id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Doctor? doctor = await _doctorService.GetByIdAsync(Id);
        if (doctor == null)
        {
            return RedirectToAction(nameof(Index));
        }

        doctor.Appointments = await _appointmentService.GetAllAppointmentsByDoctorIdAsync(doctor.Id);
        doctor.Hospitals = await _hospitalsDoctorsService.GetByDoctorIdAsync(doctor.Id) ?? [];

        return View(doctor);
    }
}
