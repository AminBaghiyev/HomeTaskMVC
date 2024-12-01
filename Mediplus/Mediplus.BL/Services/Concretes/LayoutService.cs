using Mediplus.BL.Services.Abstractions;

namespace Mediplus.BL.Services.Concretes;

public class LayoutService : ILayoutService
{
    readonly ISettingService _settingService;

    public LayoutService(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public async Task<Dictionary<string, string?>> GetSettingsAsync()
    {
        return await _settingService.GetAllActiveSettingAsync();
    }
}
