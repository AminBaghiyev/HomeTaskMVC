using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Abstractions;

public interface IAppointmentService : IBaseService<Appointment>
{
    Task<List<Appointment>> GetAllAppointmentsByPatientIdAsync(int id);
    Task<List<Appointment>> GetAllAppointmentsByPatientIdAsNoTracking(int id);
    Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(int id);
    Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsNoTracking(int id);
    Task<bool> CheckDoctorAvailability(int id, DateTime appointmentDate, int patientId = 0);
}
