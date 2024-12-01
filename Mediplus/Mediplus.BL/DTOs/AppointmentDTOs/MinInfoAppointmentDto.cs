namespace Mediplus.BL.DTOs.AppointmentDTOs;

public class MinInfoAppointmentDto
{
    public int Id { get; set; }
    public string PatientFullname { get; set; }
    public string DoctorFullname { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsActive { get; set; }
}
