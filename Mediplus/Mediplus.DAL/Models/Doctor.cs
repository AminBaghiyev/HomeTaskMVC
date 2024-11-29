using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Doctor : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsWorking { get; set; }
    public int Salary { get; set; }
    public ICollection<Appointment> Appointments { get; } = [];
}
