using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.SliderItemDTOs;

public class ShowSliderItemDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? MainUrl { get; set; }
    public string? SecondUrl { get; set; }
    public string BackgroundImagePath { get; set; }

    public static implicit operator ShowSliderItemDto(SliderItem item)
    {
        return new ShowSliderItemDto
        {
            Title = item.Title,
            Description = item.Description,
            MainUrl = item.MainUrl,
            SecondUrl = item.SecondUrl,
            BackgroundImagePath = item.BackgroundImagePath
        };
    }
}
