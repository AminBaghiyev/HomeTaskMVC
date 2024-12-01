using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.AppointmentDTOs;

public class FormAdminAppointmentDto
{
    public int Id { get; set; }
    public int? DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Message { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator FormAdminAppointmentDto(Appointment item)
    {
        return new FormAdminAppointmentDto()
        {
            Id = item.Id,
            DoctorId = item.DoctorId,
            PatientId = item.PatientId,
            AppointmentDate = item.AppointmentDate,
            Message = item.Message,
            IsActive = item.IsActive
        };
    }
}
