using Inance.Models;

namespace Inance.DTOs.ServiceDTOs;

public class GetServiceDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Master> Masters { get; set; }
    public ICollection<Order> Orders { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
