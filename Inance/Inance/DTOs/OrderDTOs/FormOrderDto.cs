using Inance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Inance.DTOs.OrderDTOs;

public class FormOrderDto
{
    public int Id { get; set; }

    [Display(Name = "Client Name")]
    public string ClientName { get; set; }

    [Display(Name = "Client Surname")]
    public string ClientSurname { get; set; }

    [Display(Name = "Client Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string ClientPhoneNumber { get; set; }

    [Display(Name = "Client E-mail")]
    [DataType(DataType.EmailAddress)]
    public string ClientEmail { get; set; }

    [Required]
    public int ServiceId { get; set; }
    public int MasterId { get; set; }
    public string Problem { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator FormOrderDto(Order item)
    {
        return new FormOrderDto()
        {
            Id = item.Id,
            ClientName = item.ClientName,
            ClientSurname = item.ClientSurname,
            ClientPhoneNumber = item.ClientPhoneNumber,
            ClientEmail = item.ClientEmail,
            ServiceId = item.ServiceId,
            MasterId = item.MasterId,
            Problem = item.Problem,
            IsActive = item.IsActive
        };
    }
}
