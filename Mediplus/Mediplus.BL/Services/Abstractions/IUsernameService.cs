using Mediplus.DAL.Models.Base;

namespace Mediplus.BL.Services.Abstractions;

public interface IUsernameService<T> : IBaseService<T> where T : BaseEntity, new()
{
	Task<T?> GetByUsernameAsync(string username);
	Task<T?> GetByUsernameAsNoTrackingAsync(string username);
}
