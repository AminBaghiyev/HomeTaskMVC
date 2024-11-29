using Mediplus.BL.DTOs.PartnerDTOs;
using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Concretes;

public interface IPartnerService
{
	Task<IEnumerable<ShowPartnerDto>> GetAllShowedPartnersAsync();
	Task<List<Partner>> GetAllPartnersAsync();
	Task<Partner?> GetPartnerByIdAsync(int Id);
	Task CreatePartnerAsync(Partner partner);
	Task UpdatePartnerAsync(int Id, Partner partner);
	Task DeletePartnerAsync(int Id);
}
