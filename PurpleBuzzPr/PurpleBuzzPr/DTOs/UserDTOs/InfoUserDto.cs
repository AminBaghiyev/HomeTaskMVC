using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.DTOs.UserDTOs;

public class InfoUserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public static implicit operator InfoUserDto(AppUser? appUser)
    {
        return new InfoUserDto
        {
            FirstName = appUser?.FirstName,
            LastName = appUser?.LastName,
            Email = appUser?.Email,
            UserName = appUser?.UserName
        };
    }
}
