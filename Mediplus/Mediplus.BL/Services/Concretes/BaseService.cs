using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Mediplus.BL.Services.Concretes;

public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
{
    protected readonly AppDbContext _db;

    public BaseService(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(T item)
    {
        PropertyInfo? createdAtProp = typeof(T).GetProperty("CreatedAt");
        if (createdAtProp is not null) createdAtProp.SetValue(item, DateTime.Now);

        await _db.Set<T>().AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        T? item = await GetByIdAsync(id);
        if (item is null) return;

        _db.Set<T>().Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync(int limit = 0)
    {
        if (limit == 0) return await _db.Set<T>().AsNoTracking().ToListAsync();
        else return await _db.Set<T>().AsNoTracking().Take(limit).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetByIdAsNoTrackingAsync(int id)
    {
        return await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task UpdateAsync(int id, T updatedItem)
    {
        T? item = await GetByIdAsNoTrackingAsync(id);
        if (item is null) return;

        PropertyInfo? updatedAtProp = typeof(T).GetProperty("UpdatedAt");
        if (updatedAtProp is not null) updatedAtProp.SetValue(updatedItem, DateTime.Now);
        PropertyInfo? createdAtProp = typeof(T).GetProperty("CreatedAt");
        if (createdAtProp is not null) createdAtProp.SetValue(updatedItem, createdAtProp.GetValue(item));

        _db.Set<T>().Update(updatedItem);
        await _db.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllActiveAsync(int limit = 0)
    {
        PropertyInfo? isActiveProp = typeof(T).GetProperty("IsActive");

        if (isActiveProp is not null && isActiveProp.PropertyType == typeof(bool))
        {
            if (limit == 0) return await _db.Set<T>().Where(i => EF.Property<bool>(i, "IsActive") == true).ToListAsync();
            else return await _db.Set<T>().Where(i => EF.Property<bool>(i, "IsActive") == true).Take(limit).ToListAsync();
        }

        else return await GetAllAsync(limit: limit);
    }
}
