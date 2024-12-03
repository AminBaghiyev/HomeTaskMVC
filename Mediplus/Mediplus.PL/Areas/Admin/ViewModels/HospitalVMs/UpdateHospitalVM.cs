using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Mediplus.DAL.Models;

namespace Mediplus.PL.Areas.Admin.ViewModels;

public class UpdateHospitalVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    [Display(Name = "Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    [Display(Name = "E-mail")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public ICollection<int>? DoctorsIds { get; set; }

    [ValidateNever]
    public SelectList DoctorsList { get; set; }

    public static implicit operator UpdateHospitalVM(Hospital item)
    {
        return new UpdateHospitalVM()
        {
            Id = item.Id,
            Name = item.Name,
            Address = item.Address,
            PhoneNumber = item.PhoneNumber,
            Email = item.Email,
            IsActive = item.IsActive
        };
    }
}
