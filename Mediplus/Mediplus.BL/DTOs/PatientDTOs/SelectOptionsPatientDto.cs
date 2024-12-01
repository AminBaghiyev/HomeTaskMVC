using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.PatientDTOs;

public class SelectOptionsPatientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Fullname => $"{Surname} {Name}";

    public static implicit operator SelectOptionsPatientDto(Patient item)
    {
        return new SelectOptionsPatientDto()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname
        };
    }
}
