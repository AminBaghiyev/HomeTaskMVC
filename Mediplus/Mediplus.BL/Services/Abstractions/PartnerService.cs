using Mediplus.BL.DTOs.PartnerDTOs;
using Mediplus.BL.Services.Concretes;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Abstractions;

public class PartnerService : IPartnerService
{
	readonly AppDbContext _db;
	public PartnerService(AppDbContext db)
	{
		_db = db;
	}

	public async Task CreatePartnerAsync(Partner partner)
	{
		partner.CreatedAt = DateTime.Now;
		await _db.Partners.AddAsync(partner);
		await _db.SaveChangesAsync();
	}

	public async Task DeletePartnerAsync(int Id)
	{
		Partner? partner = await GetPartnerByIdAsync(Id);
		if (partner == null)
		{
			return;
		}

		_db.Partners.Remove(partner);
		await _db.SaveChangesAsync();
	}

	public async Task<List<Partner>> GetAllPartnersAsync() => await _db.Partners.ToListAsync();

	public async Task<Partner?> GetPartnerByIdAsync(int Id) => await _db.Partners.FindAsync(Id);

	public async Task<IEnumerable<ShowPartnerDto>> GetAllShowedPartnersAsync()
	{
		return await _db.Partners
			.AsNoTracking()
			.Select(item => new ShowPartnerDto
			{
				Title = item.Title,
				Url = item.Url,
				LogoPath = item.LogoPath
			})
			.ToListAsync();
	}

	public async Task UpdatePartnerAsync(int Id, Partner updatedPartner)
	{
		Partner? partner = await GetPartnerByIdAsync(Id);
		if (partner == null)
		{
			return;
		}

		partner.Title = updatedPartner.Title;
		partner.Url = updatedPartner.Url;
		partner.LogoPath = updatedPartner.LogoPath;
		partner.UpdatedAt = DateTime.Now;

		await _db.SaveChangesAsync();
	}
}
