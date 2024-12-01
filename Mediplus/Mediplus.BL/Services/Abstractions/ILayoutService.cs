namespace Mediplus.BL.Services.Abstractions;

public interface ILayoutService
{
    Task<Dictionary<string, string?>> GetSettingsAsync();
}
