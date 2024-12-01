using Mediplus.BL.DTOs.BlogDTOs;
using Mediplus.BL.DTOs.PartnerDTOs;
using Mediplus.BL.DTOs.PortfolioDTOs;
using Mediplus.BL.DTOs.SliderItemDTOs;
using Mediplus.DAL.Models;
using Mediplus.PL.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Mediplus.BL.DTOs.AppointmentDTOs;
using Mediplus.BL.Services.Abstractions;

namespace Mediplus.PL.Controllers;

public class HomeController : Controller
{
    readonly IBaseService<SliderItem> _sliderItemService;
    readonly IBaseService<Portfolio> _portfolioService;
    readonly IBaseService<Partner> _partnerService;
    readonly IBaseService<Blog> _blogService;
    readonly IUsernameService<Patient> _patientService;
    readonly IBaseService<Appointment> _appointmentService;
	readonly ISettingService _settingService;

    public HomeController(IBaseService<Appointment> appointmentService, IUsernameService<Patient> patientService, IBaseService<SliderItem> sliderItemService, IBaseService<Portfolio> portfolioService, IBaseService<Partner> partnerService, IBaseService<Blog> blogService, ISettingService settingService)
    {
        _sliderItemService = sliderItemService;
        _portfolioService = portfolioService;
        _partnerService = partnerService;
        _blogService = blogService;
        _settingService = settingService;
        _patientService = patientService;
        _appointmentService = appointmentService;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM VM = new()
        {
            SliderItems = (await _sliderItemService.GetAllActiveAsync()).Select(i => (ShowSliderItemDto) i).ToList(),
            PortfolioCards = (await _portfolioService.GetAllActiveAsync()).Select(i => (ShowPortfolioCardDto) i).ToList(),
            Partners = (await _partnerService.GetAllActiveAsync()).Select(i => (ShowPartnerDto) i).ToList(),
            Blogs = (await _blogService.GetAllActiveAsync(3)).Select(i => (ShowBlogDto) i).ToList(),
            Settings = await _settingService.GetAllActiveSettingAsync()
        };

        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookAppointment(HomeVM VM)
    {
        FormUserAppointmentDto form = VM.AppointmentForm;

		ModelState.Clear();
		TryValidateModel(form, nameof(form));

		if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        Patient? patient = await _patientService.GetByUsernameAsNoTrackingAsync(form.Name + form.FINCode);
        if (patient == null)
        {
            await _patientService.CreateAsync(new ()
            {
                Name = form.Name,
                Surname = form.Surname,
                FINCode = form.FINCode,
                PhoneNumber = form.PhoneNumber,
                Username = form.Name + form.FINCode,
                IsActive = true
			});

            patient = await _patientService.GetByUsernameAsNoTrackingAsync(form.Name + form.FINCode);
        }

        await _appointmentService.CreateAsync(new ()
        {
            PatientId = patient.Id,
            Message = form.Message
        });

        return RedirectToAction(nameof(Index));
    }
}
