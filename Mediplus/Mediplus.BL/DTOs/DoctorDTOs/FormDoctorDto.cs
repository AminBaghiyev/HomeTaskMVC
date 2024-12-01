using Mediplus.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Mediplus.BL.DTOs.DoctorDTOs;

public class FormDoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    [Display(Name = "FIN Code")]
    public string FINCode { get; set; }

    [Display(Name = "Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator FormDoctorDto(Doctor item)
    {
        return new FormDoctorDto()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname,
            FINCode = item.FINCode,
            PhoneNumber = item.PhoneNumber,
            Email = item.Email,
            IsActive = item.IsActive
        };
    }
}
