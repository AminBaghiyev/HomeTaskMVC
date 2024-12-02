using Inance.Models;
using System.ComponentModel.DataAnnotations;

namespace Inance.DTOs.MasterDTOs;

public class GetMasterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Fullname => $"{Surname} {Name}";
    public string Username { get; set; }

    [Display(Name = "Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Display(Name = "E-mail")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Experience Year")]
    public int ExperienceYear { get; set; }
    public int ServiceId { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator GetMasterDto(Master item)
    {
        return new GetMasterDto()
        {
            Id = item.Id,
            Name = item.Name,
            Surname = item.Surname,
            Username = item.Username,
            PhoneNumber = item.PhoneNumber,
            Email = item.Email,
            ExperienceYear = item.ExperienceYear,
            ServiceId = item.ServiceId,
            IsActive = item.IsActive
        };
    }
}
