using Mediplus.DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mediplus.DAL.Models;

public class Doctor : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    [NotMapped]
    public string Fullname => $"Dr. {Surname} {Name}";
    public string Username { get; set; }
    public string FINCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<HospitalsDoctors> HospitalsDoctors { get; set; }
    public ICollection<Hospital>? Hospitals { get; set; }
}
