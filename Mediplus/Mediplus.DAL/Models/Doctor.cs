using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Doctor : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string FINCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
