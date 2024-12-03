﻿using Mediplus.DAL.Models.Base;

namespace Mediplus.DAL.Models;

public class Hospital : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public ICollection<HospitalsDoctors> HospitalsDoctors { get; set; }
    public ICollection<Doctor> Doctors { get; set; }
}
