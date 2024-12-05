using Inance.Models;

namespace Inance.DTOs.ServiceDTOs;

public class UpdateServiceDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ThumbnailPath { get; set; }
    public IFormFile? Thumbnail { get; set; }
    public ICollection<ServicePhoto>? PhotosPaths { get; set; }
    public List<IFormFile>? Photos { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator UpdateServiceDto(Service item)
    {
        return new UpdateServiceDto()
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            ThumbnailPath = item.ThumbnailPath,
            PhotosPaths = item.Photos,
            IsActive = item.IsActive,
        };
    }
}
