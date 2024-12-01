using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Concretes;

public class SettingService : ISettingService
{
    readonly AppDbContext _db;

    public SettingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddSettingAsync(Setting setting)
    {
        await _db.Settings.AddAsync(setting);
        await _db.SaveChangesAsync();
    }

    public async Task<Dictionary<string, string?>> GetAllActiveSettingAsync()
    {
        return await _db.Settings.Where(setting => setting.IsActive).ToDictionaryAsync(setting => setting.Key, setting => setting.Value);
    }

    public async Task<List<Setting>> GetAllSettingsAsync()
    {
        return await _db.Settings.AsNoTracking().ToListAsync();
    }

    public async Task<Setting?> GetSettingByIdAsync(int id)
    {
        return await _db.Settings.FindAsync(id);
    }

    public async Task<Setting?> GetSettingByKeyAsync(string key)
    {
        return await _db.Settings.FirstOrDefaultAsync(x => x.Key == key);
    }

    public async Task SetActiveSettingAsync(string key)
    {
        Setting? setting = await GetSettingByKeyAsync(key);
        if (setting is null)
        {
            return;
        }

        setting.IsActive = true;
        await _db.SaveChangesAsync();
    }

    public async Task SetActiveSettingAsync(int id)
    {
        Setting? setting = await GetSettingByIdAsync(id);
        if (setting is null)
        {
            return;
        }

        setting.IsActive = true;
        await _db.SaveChangesAsync();
    }

    public async Task SetDeactiveSettingAsync(string key)
    {
        Setting? setting = await GetSettingByKeyAsync(key);
        if (setting is null)
        {
            return;
        }

        setting.IsActive = false;
        await _db.SaveChangesAsync();
    }

    public async Task SetDeactiveSettingAsync(int id)
    {
        Setting? setting = await GetSettingByIdAsync(id);
        if (setting is null)
        {
            return;
        }

        setting.IsActive = false;
        await _db.SaveChangesAsync();
    }

    public async Task UpdateSettingAsync(int id, Setting newSetting)
    {
        Setting? setting = await GetSettingByIdAsync(id);
        if (setting is null)
        {
            return;
        }

        setting.Value = newSetting.Value;

        await _db.SaveChangesAsync();
    }
}
