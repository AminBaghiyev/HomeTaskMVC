using Mediplus.DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mediplus.DAL.Models;

public class Appointment : BaseAuditableEntity
{
	public int? DoctorId { get; set; }
    [NotMapped]
	public Doctor? Doctor { get; set; }
	public int PatientId { get; set; }
	[NotMapped]
	public Patient Patient { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
    public string Message { get; set; }
	public bool IsActive { get; set; }
}
