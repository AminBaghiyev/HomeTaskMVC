using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.PatientDTOs;

public class MinInfoPatientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator MinInfoPatientDto(Patient item)
    {
        return new MinInfoPatientDto()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname,
            Username = item.Username,
            PhoneNumber = item.PhoneNumber,
            IsActive = item.IsActive
        };
    }
}
