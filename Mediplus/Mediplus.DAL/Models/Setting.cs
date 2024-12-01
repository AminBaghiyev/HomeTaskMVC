using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Setting : BaseEntity
{
    public string Key { get; set; }
    public string? Value { get; set; }
    public bool IsActive { get; set; }
}
