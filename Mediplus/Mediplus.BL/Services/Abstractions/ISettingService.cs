using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Abstractions;

public interface ISettingService
{
    Task<Dictionary<string, string?>> GetAllActiveSettingAsync();
    Task<List<Setting>> GetAllSettingsAsync();
    Task<Setting?> GetSettingByKeyAsync(string key);
    Task<Setting?> GetSettingByIdAsync(int id);
    Task AddSettingAsync(Setting setting);
    Task UpdateSettingAsync(int id, Setting newSetting);
    Task SetDeactiveSettingAsync(string key);
    Task SetDeactiveSettingAsync(int id);
    Task SetActiveSettingAsync(string key);
    Task SetActiveSettingAsync(int id);
}
