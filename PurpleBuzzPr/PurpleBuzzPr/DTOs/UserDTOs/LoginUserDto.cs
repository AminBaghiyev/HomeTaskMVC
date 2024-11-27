using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzPr.DTOs.UserDTOs;

public class LoginUserDto
{
    [Required]
    [Display(Prompt = "Enter Username or Email")]
    public string UserNameOrEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Prompt = "Enter Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
