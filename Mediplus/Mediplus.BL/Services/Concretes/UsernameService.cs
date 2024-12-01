using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Concretes;

public class UsernameService<T> : BaseService<T>, IUsernameService<T> where T : BaseEntity, new()
{
    public UsernameService(AppDbContext db) : base (db) { }

    public async Task<T?> GetByUsernameAsNoTrackingAsync(string username)
	{
		return await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => EF.Property<string>(i, "Username") == username);
	}

	public async Task<T?> GetByUsernameAsync(string username)
	{
		return await _db.Set<T>().FirstOrDefaultAsync(i => EF.Property<string>(i, "Username") == username);
	}
}
