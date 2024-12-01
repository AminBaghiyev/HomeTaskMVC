using Mediplus.DAL.Models;

namespace Mediplus.BL.DTOs.SettingDTOs;

public class ShowSettingDto
{
	public string Key { get; set; }
	public string? Value { get; set; }

	public static implicit operator ShowSettingDto(Setting item)
	{
		return new ShowSettingDto()
		{
			Key = item.Key,
			Value = item.Value
		};
	}
}
