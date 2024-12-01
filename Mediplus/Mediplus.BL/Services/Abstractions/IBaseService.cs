using Mediplus.DAL.Models.Base;

namespace Mediplus.BL.Services.Abstractions;

public interface IBaseService<T> where T : BaseEntity, new()
{
    Task<List<T>> GetAllAsync(int limit = 0);
    Task<List<T>> GetAllActiveAsync(int limit = 0);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsNoTrackingAsync(int id);
    Task CreateAsync(T item);
    Task UpdateAsync(int id, T item);
    Task DeleteAsync(int id);
}
