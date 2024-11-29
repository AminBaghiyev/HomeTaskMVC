using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Department : BaseAuditableEntity
{
    public string Title { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<Appointment> Appointments { get; } = [];
}
