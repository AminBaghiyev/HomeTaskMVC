using Mediplus.BL.DTOs.AppointmentDTOs;
using Mediplus.BL.DTOs.DoctorDTOs;
using Mediplus.BL.DTOs.PatientDTOs;

namespace Mediplus.PL.Areas.Admin.ViewModels;

public class AppointmentVM
{
    public IEnumerable<SelectOptionsDoctorDto> Doctors { get; set; }
    public IEnumerable<SelectOptionsPatientDto> Patients { get; set; }
    public FormAdminAppointmentDto? Form { get; set; }
}
