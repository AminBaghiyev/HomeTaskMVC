namespace Inance.DTOs.ServiceDTOs;

public class CreateServiceDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? Thumbnail { get; set; }
    public List<IFormFile>? Photos { get; set; }
    public bool IsActive { get; set; }
}
