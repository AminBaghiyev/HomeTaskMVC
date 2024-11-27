using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzPr.DTOs.UserDTOs;

public class CreateUserDto
{
    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Firstname")]
    public string FirstName { get; set; }

    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Lastname")]
    public string LastName { get; set; }

    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Username")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Prompt = "Email")]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    [Display(Prompt = "Password")]
    public string Password { get; set; }

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    [Display(Prompt = "Repeat Password")]
    [Compare(nameof(Password))]
    public string RepeatedPassword { get; set; }
}
