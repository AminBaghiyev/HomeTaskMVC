using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Mediplus.PL.Areas.Admin.ViewModels;

public class CreateHospitalVM
{
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
}
