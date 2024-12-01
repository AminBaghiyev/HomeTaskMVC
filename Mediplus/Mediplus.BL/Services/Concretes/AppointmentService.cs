using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Concretes;

public class AppointmentService : BaseService<Appointment>, IAppointmentService
{
    public AppointmentService(AppDbContext db) : base(db) { }

    public async Task<bool> CheckDoctorAvailability(int doctorId, DateTime appointmentDate, int appointmentId = 0)
    {
        //var a = _db.Doctors.Join(_db.Appointments,
        //    doctor => doctor.Id,
        //    appointment => appointment.DoctorId,
        //    (doctor, appointment) => new { DoctorId = doctor.Id, DoctorIsActive = doctor.IsActive, AppointmentId = appointment.Id, Date = appointment.AppointmentDate }
        //).Where(i =>
        //    i.DoctorIsActive &&
        //    i.DoctorId == doctorId &&
        //    i.AppointmentId != appointmentId &&
        //    ((i.Date.Date == appointmentDate.Date &&
        //    i.Date.Hour == appointmentDate.Hour) ||
        //    ((i.Date.Date == appointmentDate.Date &&
        //    EF.Functions.DateDiffMinute(i.Date, appointmentDate) <= 15) &&
        //    (i.Date.Date == appointmentDate.Date &&
        //    EF.Functions.DateDiffMinute(appointmentDate, i.Date) <= 15)))
        //).ToList();
        //return false;

        return ! await _db.Doctors.Join(_db.Appointments,
            doctor => doctor.Id,
            appointment => appointment.DoctorId,
            (doctor, appointment) => new { DoctorId = doctor.Id, DoctorIsActive = doctor.IsActive, AppointmentId = appointment.Id, Date = appointment.AppointmentDate }
        ).Where(i =>
            i.DoctorIsActive &&
            i.DoctorId == doctorId &&
            i.AppointmentId != appointmentId &&
            ((i.Date.Date == appointmentDate.Date &&
            i.Date.Hour == appointmentDate.Hour) ||
            ((i.Date.Date == appointmentDate.Date &&
            EF.Functions.DateDiffMinute(i.Date, appointmentDate) <= 15) &&
            (i.Date.Date == appointmentDate.Date &&
            EF.Functions.DateDiffMinute(appointmentDate, i.Date) <= 15)))
        ).AnyAsync();
    }

    public async Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsNoTracking(int id)
    {
        return await _db.Appointments.Where(a => a.DoctorId == id).AsNoTracking().ToListAsync();
    }

    public async Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(int id)
    {
        return await _db.Appointments.Where(a => a.DoctorId == id).ToListAsync();
    }

    public async Task<List<Appointment>> GetAllAppointmentsByPatientIdAsNoTracking(int id)
    {
        return await _db.Appointments.Where(a => a.PatientId == id).AsNoTracking().ToListAsync();
    }

    public async Task<List<Appointment>> GetAllAppointmentsByPatientIdAsync(int id)
    {
        return await _db.Appointments.Where(a => a.PatientId == id).ToListAsync();
    }
}
