using Mediplus.BL.DTOs.AppointmentDTOs;
using Mediplus.BL.DTOs.DoctorDTOs;
using Mediplus.BL.DTOs.PatientDTOs;
using Mediplus.DAL.Models;

namespace Mediplus.PL.Areas.Admin.ViewModels.Appointment;

public class AppointmentVM
{
    public IEnumerable<SelectOptionsDoctorDto> Doctors { get; set; }
    public IEnumerable<SelectOptionsPatientDto> Patients { get; set; }
    public FormAdminAppointmentDto? Form { get; set; }
}
