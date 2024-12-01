using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.PatientDTOs;

public class FormPatientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FINCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator FormPatientDto(Patient item)
    {
        return new FormPatientDto()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname,
            FINCode = item.FINCode,
            PhoneNumber = item.PhoneNumber,
            IsActive = item.IsActive
        };
    }
}
