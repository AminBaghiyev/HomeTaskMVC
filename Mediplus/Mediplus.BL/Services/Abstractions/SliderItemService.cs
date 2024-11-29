using Mediplus.BL.DTOs.SliderItemDTOs;
using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Abstractions;

public class SliderItemService : ISliderItemService
{
    readonly AppDbContext _db;
    public SliderItemService(AppDbContext db)
    {
        _db = db;
    }

    public async Task DeleteSliderItemAsync(int Id)
    {
        SliderItem? sliderItem = await GetSliderItemByIdAsync(Id);
        if (sliderItem == null)
        {
            return;
        }

        _db.SliderItems.Remove(sliderItem);
        await _db.SaveChangesAsync();
    }

    public async Task<List<SliderItem>> GetAllSliderItemsAsync() => await _db.SliderItems.ToListAsync();

    public async Task<SliderItem?> GetSliderItemByIdAsync(int Id) => await _db.SliderItems.FindAsync(Id);

    public async Task UpdateSliderItemAsync(int Id, SliderItem updatedSliderItem)
    {
        SliderItem? sliderItem = await GetSliderItemByIdAsync(Id);
        if (sliderItem == null)
        {
            return;
        }

        sliderItem.Title = updatedSliderItem.Title;
        sliderItem.Description = updatedSliderItem.Description;
        sliderItem.MainUrl = updatedSliderItem.MainUrl;
        sliderItem.SecondUrl = updatedSliderItem.SecondUrl;
        sliderItem.BackgroundImagePath = updatedSliderItem.BackgroundImagePath;
        sliderItem.IsActive = updatedSliderItem.IsActive;
        sliderItem.UpdatedAt = DateTime.Now;

        await _db.SaveChangesAsync();
    }

    public async Task CreateSliderItemAsync(SliderItem sliderItem)
    {
        sliderItem.CreatedAt = DateTime.Now;
        await _db.SliderItems.AddAsync(sliderItem);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<ShowSliderItemDto>> GetAllShowedSliderItemsAsync()
    {
        return await _db.SliderItems
            .AsNoTracking()
            .Where(item => item.IsActive)
            .Select(item => new ShowSliderItemDto
            {
                Title = item.Title,
                Description = item.Description,
                MainUrl = item.MainUrl,
                SecondUrl = item.SecondUrl,
                BackgroundImagePath = item.BackgroundImagePath
            })
            .ToListAsync();
    }
}
