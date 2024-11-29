using Mediplus.BL.DTOs.SliderItemDTOs;
using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Concretes;

public interface ISliderItemService
{
    Task<IEnumerable<ShowSliderItemDto>> GetAllShowedSliderItemsAsync();
    Task<List<SliderItem>> GetAllSliderItemsAsync();
    Task<SliderItem?> GetSliderItemByIdAsync(int Id);
    Task CreateSliderItemAsync(SliderItem sliderItem);
    Task UpdateSliderItemAsync(int Id, SliderItem sliderItem);
    Task DeleteSliderItemAsync(int Id);
}
