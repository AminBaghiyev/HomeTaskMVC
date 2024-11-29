using System.ComponentModel.DataAnnotations;

namespace Mediplus.BL.DTOs.UserDTOs;

public class RegisterUserDto
{
    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Enter Firstname")]
    public string FirstName { get; set; }

    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Enter Lastname")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Prompt = "Enter Email")]
    public string Email { get; set; }

    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Enter Username")]
    public string UserName { get; set; }

    [Required]
    [MinLength(4)]
    [DataType(DataType.Password)]
    [Display(Prompt = "Enter Password")]
    public string Password { get; set; }

    [Required]
    [MinLength(4)]
    [DataType(DataType.Password)]
    [Display(Prompt = "Repeat Password")]
    [Compare(nameof(Password))]
    public string RepeatedPassword { get; set; }
}
