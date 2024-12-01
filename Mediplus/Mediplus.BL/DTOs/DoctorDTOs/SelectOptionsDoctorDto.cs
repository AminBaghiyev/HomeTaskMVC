using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.DoctorDTOs;

public class SelectOptionsDoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Fullname => $"Dr. {Surname} {Name}";

    public static implicit operator SelectOptionsDoctorDto(Doctor item)
    {
        return new SelectOptionsDoctorDto()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname
        };
    }
}
