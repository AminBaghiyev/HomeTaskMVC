using Mediplus.BL.DTOs.AppointmentDTOs;
using Mediplus.BL.DTOs.DoctorDTOs;
using Mediplus.BL.DTOs.PatientDTOs;
using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Models;
using Mediplus.PL.Areas.Admin.ViewModels.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace Mediplus.PL.Areas.Admin.Controllers;

[Area("Admin")]
public class AppointmentController : Controller
{
    readonly IAppointmentService _appointmentService;
    readonly IBaseService<Doctor> _doctorService;
    readonly IBaseService<Patient> _patientService;

    public AppointmentController(IAppointmentService appointmentService, IBaseService<Doctor> doctorService, IBaseService<Patient> patientService)
    {
        _appointmentService = appointmentService;
        _doctorService = doctorService;
        _patientService = patientService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Appointment> appointments = await _appointmentService.GetAllAsync();
        ICollection<MinInfoAppointmentDto> minInfoAppointments = [];
        foreach (Appointment item in appointments)
        {
            if (item.DoctorId is not null) item.Doctor = await _doctorService.GetByIdAsync((int) item.DoctorId);
            item.Patient = await _patientService.GetByIdAsync(item.PatientId);
            minInfoAppointments.Add(new ()
            {
                Id = item.Id,
                PatientFullname = item.Patient.Name + " " + item.Patient.Surname,
                DoctorFullname = item?.Doctor?.Name + " " + item?.Doctor?.Surname,
                AppointmentDate = item.AppointmentDate,
                IsActive = item.IsActive
            });
        }

        return View(minInfoAppointments);
    }

    public async Task<IActionResult> Create()
    {
        AppointmentVM appointmentVM = new()
        {
            Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto) i).ToList(),
            Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto) i).ToList()
        };

        return View(appointmentVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AppointmentVM VM)
    {
        FormAdminAppointmentDto form = VM.Form;

        ModelState.Clear();
        TryValidateModel(form);

        if (!ModelState.IsValid)
        {
            AppointmentVM appointmentVM = new()
            {
                Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
                Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
                Form = form
            };
            return View(appointmentVM);
        }

        if (form.AppointmentDate.Year <= DateTime.Now.Year && form.AppointmentDate.DayOfYear <= DateTime.Now.DayOfYear)
        {
            ModelState.AddModelError("Form.AppointmentDate", "A future date should be selected!");
            AppointmentVM appointmentVM = new()
            {
                Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
                Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
                Form = form
            };
            return View(appointmentVM);
        }

        bool isAvailable = await _appointmentService.CheckDoctorAvailability((int) form.DoctorId, form.AppointmentDate);

        if (!isAvailable)
        {
            ModelState.AddModelError("Form.AppointmentDate", "Doctor isn't available!");
            AppointmentVM appointmentVM = new()
            {
                Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
                Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
                Form = form
            };
            return View(appointmentVM);
        }

        Appointment appointment = new()
        {
            DoctorId = form.DoctorId,
            PatientId = form.PatientId,
            AppointmentDate = form.AppointmentDate,
            Message = form.Message,
            IsActive = form.IsActive
        };

        await _appointmentService.CreateAsync(appointment);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Appointment? appointment = await _appointmentService.GetByIdAsync(Id);
        if (appointment == null)
        {
            return RedirectToAction(nameof(Index));
        }

        AppointmentVM appointmentVM = new()
        {
            Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
            Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
            Form = appointment
        };

        return View(appointmentVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AppointmentVM VM)
    {
        FormAdminAppointmentDto form = VM.Form;

        ModelState.Clear();
        TryValidateModel(form);

        if (!ModelState.IsValid)
        {
            AppointmentVM appointmentVM = new()
            {
                Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
                Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
                Form = form
            };
            return View(appointmentVM);
        }

        if (form.AppointmentDate.Year <= DateTime.Now.Year && form.AppointmentDate.DayOfYear <= DateTime.Now.DayOfYear)
        {
            ModelState.AddModelError("Form.AppointmentDate", "A future date should be selected!");
            AppointmentVM appointmentVM = new()
            {
                Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
                Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
                Form = form
            };
            return View(appointmentVM);
        }

        bool isAvailable = await _appointmentService.CheckDoctorAvailability((int) form.DoctorId, form.AppointmentDate, form.Id);

        if (!isAvailable)
        {
            ModelState.AddModelError("Form.AppointmentDate", "Doctor isn't available!");
            AppointmentVM appointmentVM = new()
            {
                Doctors = (await _doctorService.GetAllActiveAsync()).Select(i => (SelectOptionsDoctorDto)i).ToList(),
                Patients = (await _patientService.GetAllActiveAsync()).Select(i => (SelectOptionsPatientDto)i).ToList(),
                Form = form
            };
            return View(appointmentVM);
        }

        Appointment appointment = new()
        {
            Id = form.Id,
            DoctorId = form.DoctorId,
            PatientId = form.PatientId,
            AppointmentDate = form.AppointmentDate,
            Message = form.Message,
            IsActive = form.IsActive
        };

        await _appointmentService.UpdateAsync(form.Id, appointment);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int Id)
    {
        await _appointmentService.DeleteAsync(Id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int Id)
    {
        Appointment? appointment = await _appointmentService.GetByIdAsync(Id);
        if (appointment == null)
        {
            return RedirectToAction(nameof(Index));
        }

        appointment.Doctor = appointment.DoctorId is not null ?
            await _doctorService.GetByIdAsNoTrackingAsync((int)appointment.DoctorId) :
            null;

        appointment.Patient = await _patientService.GetByIdAsNoTrackingAsync(appointment.PatientId) ?? new();

        return View(appointment);
    }
}
