namespace Mediplus.DAL.Models;

public class HospitalsDoctors
{
    public int HospitalId { get; set; }
    public int DoctorId { get; set; }
    public Hospital? Hospital { get; set; }
    public Doctor? Doctor { get; set; }
}
