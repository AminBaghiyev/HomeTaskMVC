using Inance.Models;

namespace Inance.DTOs.OrderDTOs;

public class GetOrderDto
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string ClientSurname { get; set; }
    public string ClientFullname => $"{ClientSurname} {ClientName}";
    public string ClientPhoneNumber { get; set; }
    public string ClientEmail { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator GetOrderDto(Order item)
    {
        return new GetOrderDto()
        {
            Id = item.Id,
            ClientName = item.ClientName,
            ClientSurname = item.ClientSurname,
            ClientPhoneNumber = item.ClientPhoneNumber,
            ClientEmail = item.ClientEmail,
            IsActive = item.IsActive
        };
    }
}
