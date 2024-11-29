using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Appointment : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public string Message { get; set; }
    public DateTime? EndDate { get; set; }
}
