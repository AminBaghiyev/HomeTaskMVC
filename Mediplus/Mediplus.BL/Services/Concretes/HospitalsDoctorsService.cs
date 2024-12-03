using Mediplus.BL.Services.Abstractions;
using Mediplus.DAL.Contexts;
using Mediplus.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.BL.Services.Concretes;

public class HospitalsDoctorsService : IHospitalsDoctorsService
{
    readonly AppDbContext _db;

    public HospitalsDoctorsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(HospitalsDoctors item)
    {
        await _db.HospitalsDoctors.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int hospitalId, int doctorId)
    {
        _db.HospitalsDoctors.Remove(new HospitalsDoctors
        {
            HospitalId = hospitalId,
            DoctorId = doctorId
        });

        await _db.SaveChangesAsync();
    }

    public async Task<List<Hospital?>> GetByDoctorIdAsync(int doctorId)
    {
        return await _db.HospitalsDoctors
            .Where(e => e.DoctorId == doctorId)
            .Select(e => e.Hospital)
            .ToListAsync();
    }

    public async Task<List<Doctor?>> GetByHospitalIdAsync(int hospitalId)
    {
        return await _db.HospitalsDoctors
            .Where(e => e.HospitalId == hospitalId)
            .Select(e => e.Doctor)
            .ToListAsync();
    }

    public async Task<List<int>> GetIdsByDoctorIdAsync(int doctorId)
    {
        return await _db.HospitalsDoctors
            .Where(e => e.DoctorId == doctorId)
            .Select(e => e.HospitalId)
            .ToListAsync();
    }

    public async Task<List<int>> GetIdsByHospitalIdAsync(int hospitalId)
    {
        return await _db.HospitalsDoctors
            .Where(e => e.HospitalId == hospitalId)
            .Select(e => e.DoctorId)
            .ToListAsync();
    }
}
