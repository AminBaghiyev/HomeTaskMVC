using System.ComponentModel.DataAnnotations;

namespace Mediplus.BL.DTOs.UserDTOs;

public class LoginUserDto
{
    [Required]
    [Length(3, 50)]
    [Display(Prompt = "Enter Username")]
    public string UserName { get; set; }

    [Required]
    [MinLength(4)]
    [DataType(DataType.Password)]
    [Display(Prompt = "Enter Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
