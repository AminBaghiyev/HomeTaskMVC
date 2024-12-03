using Mediplus.DAL.Models;

namespace Mediplus.BL.Services.Abstractions;

public interface IHospitalsDoctorsService
{
    Task<List<Doctor?>> GetByHospitalIdAsync(int hospitalId);
    Task<List<int>> GetIdsByHospitalIdAsync(int hospitalId);
    Task<List<Hospital?>> GetByDoctorIdAsync(int doctorId);
    Task<List<int>> GetIdsByDoctorIdAsync(int doctorId);
    Task CreateAsync(HospitalsDoctors item);
    Task DeleteAsync(int hospitalId, int doctorId);
}
