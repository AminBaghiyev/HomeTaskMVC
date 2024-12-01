using Mediplus.BL.DTOs.PatientDTOs;
using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class PatientController : Controller
{
    readonly IBaseService<Patient> _patientService;
    readonly IAppointmentService _appointmentService;

    public PatientController(IBaseService<Patient> patientService, IAppointmentService appointmentService)
    {
        _patientService = patientService;
        _appointmentService = appointmentService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<MinInfoPatientDto> patients = (await _patientService.GetAllAsync()).Select(i => (MinInfoPatientDto) i).ToList();

        return View(patients);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FormPatientDto item)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Patient patient = new()
        {
            Name = item.Name,
            Surname = item.Surname,
            Username = item.Name + item.FINCode,
            PhoneNumber = item.PhoneNumber,
            FINCode = item.FINCode,
            IsActive = item.IsActive
        };

        await _patientService.CreateAsync(patient);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Patient? patient = await _patientService.GetByIdAsync(Id);
        if (patient == null)
        {
            return RedirectToAction(nameof(Index));
        }

        FormPatientDto patientDto = patient;

        return View(nameof(Create), patientDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(FormPatientDto item)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Create), item);
        }

        Patient patient = new()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname,
            Username = item.Name + item.FINCode,
            PhoneNumber = item.PhoneNumber,
            FINCode = item.FINCode,
            IsActive = item.IsActive
        };

        await _patientService.UpdateAsync(item.Id, patient);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int Id)
    {
        await _patientService.DeleteAsync(Id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Patient? patient = await _patientService.GetByIdAsync(Id);
        if (patient == null)
        {
            return RedirectToAction(nameof(Index));
        }

        patient.Appointments = await _appointmentService.GetAllAppointmentsByPatientIdAsync(patient.Id);

        return View(patient);
    }
}
